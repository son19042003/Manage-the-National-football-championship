using Football_Management.Models;
using Football_Management.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Football_Management.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace Football_Management.Controllers
{
    public class AccountController : BaseController
    {
        private readonly FootballManagementContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        public AccountController(FootballManagementContext context, IEmailService emailService, IConfiguration config) : base(context)
        {
            _context = context;
            _emailService = emailService;
            _config = config;
        }

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = AuthenticateUser(model.UsernameOrEmail ?? "", model.Password ?? "");

            if (user == null)
            {
                var inactiveUser = _context.Accounts.FirstOrDefault(u =>
                    (u.Username == model.UsernameOrEmail || u.Email == model.UsernameOrEmail) && !u.IsActive);

                if (inactiveUser != null)
                {
                    ModelState.AddModelError(string.Empty, "Your account has been blocked. Please contact support.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username, email, or password.");
                }

                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.RoleName),
                new Claim("UserId", user.AccountId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            if (user.Role.RoleName == "Admin")
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            if (model.RememberMe)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddDays(7),
                    HttpOnly = true,
                    Secure = true
                };

                Response.Cookies.Append("UserLogin", user.Username, cookieOptions);
                Response.Cookies.Append("UserId", user.AccountId.ToString(), cookieOptions);
            }
            else
            {
                HttpContext.Session.SetString("UserLogin", user.Username);
                HttpContext.Session.SetString("UserId", user.AccountId.ToString());
            }

            return RedirectToAction("Index", "Home");
        }

        private Account AuthenticateUser(string usernameOrEmail, string password)
        {
            var user = _context.Accounts.Include(u => u.Role).FirstOrDefault(u => 
                (u.Username == usernameOrEmail || u.Email == usernameOrEmail) && u.IsActive);

            if (user == null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher<Account>();

            var result = passwordHasher.VerifyHashedPassword(null, user.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return user;
        }
        #endregion

        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserLogin");

            HttpContext.Session.Clear();

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["ActiveTab"] = "Accounts";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var checkUsername = await _context.Accounts
                .AnyAsync(a => a.Username == model.Username);

            if (checkUsername)
            {
                ModelState.AddModelError("Username", "Username already exists.");

                return View(model);
            }

            var checkEmail = await _context.Accounts
                .AnyAsync(a => a.Email == model.Email);

            if (checkEmail)
            {
                ModelState.AddModelError("Email", "Email already exists.");

                return View(model);
            }

            var passwordHasher = new PasswordHasher<Account>();

            var account = new Account
            {
                Username = model.Username ?? "",
                Password = passwordHasher.HashPassword(null, model.Password ?? ""),
                Email = model.Email ?? "",
                PhoneNum = model.PhoneNumber ?? "",
                FirstName = model.FirstName ?? "",
                LastName = model.LastName ?? "",
                Gender = model.Gender.ToString(),
                DateOfBirth = model.DateOfBirth,
                IsActive = true
            };

            var role = await _context.Roles.Where(r => r.RoleName == "Viewer").FirstOrDefaultAsync();

            if (role != null) account.RoleId = role.RoleId;

            if (model.AvatarFile != null && model.AvatarFile.Length > 0)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(model.AvatarFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("AvatarFile", "Only JPEG, PNG and GIF files are allowed.");
                    return View(model);
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(model.AvatarFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("AvatarFile", "Only JPEG, PNG, and GIF files are allowed.");
                    return View(model);
                }

                if (model.AvatarFile.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("AvatarFile", "File size must not exceed 10MB.");
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/account");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{model.AvatarFile.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.AvatarFile.CopyToAsync(fileStream);
                }

                account.Avatar = $"{uniqueFileName}";
            }
            else
            {
                account.Avatar = "default.png";
            }

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login" , "Account");
        }
        #endregion

        public IActionResult Profile()
        {
            string userId = HttpContext.Session.GetString("UserId")
                    ?? Request.Cookies["UserId"] ?? "";
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int accountId))
            {
                return RedirectToAction("Login", "Account");
            }

            var account = _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefault(a => a.AccountId == accountId);

            if (account == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new ProfileViewModel
            {
                AccountId = account.AccountId,
                Username = account.Username,
                AvatarUrl = account.Avatar,
                Name = account.FirstName + " " + account.LastName,
                Email = account.Email,
                PhoneNumber = account.PhoneNum,
                DayOfBirth = account.DateOfBirth,
                Gender = account.Gender,
                RoleName = account.Role.RoleName
            };

            return View(model);
        }

        #region Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var account = _context.Accounts
                .FirstOrDefault(a => a.AccountId == id);

            if (account == null)
            {
                return RedirectToAction("Login", "Account");
            }

            GenderEnum gender = GenderEnum.Unspecified;
            Enum.TryParse(account.Gender, out gender);

            var viewModel = new EditViewModel
            {
                AccountId = account.AccountId,
                Email = account.Email,
                PhoneNumber = account.PhoneNum,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Gender = gender,
                DateOfBirth = account.DateOfBirth,
                AvatarUrl = account.Avatar
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var checkEmail = await _context.Accounts
                .AnyAsync(a => a.Email == model.Email && a.AccountId != model.AccountId);

            if (checkEmail)
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(model);
            }

            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == model.AccountId);

            if (account == null)
            {
                return RedirectToAction("Profile", "Account");
            }

            if (model.AvatarFile != null && model.AvatarFile.Length > 0)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(model.AvatarFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("AvatarFile", "Only JPEG, PNG and GIF files are allowed.");
                    return View(model);
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(model.AvatarFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("AvatarFile", "Only JPEG, PNG, and GIF files are allowed.");
                    return View(model);
                }

                if (model.AvatarFile.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("AvatarFile", "File size must not exceed 10MB.");
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/account");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{model.AvatarFile.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.AvatarFile.CopyToAsync(fileStream);
                }

                account.Avatar = $"{uniqueFileName}";
            }

            account.Email = model.Email ?? "";
            account.PhoneNum = model.PhoneNumber ?? "";
            account.FirstName = model.FirstName ?? "";
            account.LastName = model.LastName ?? "";
            account.DateOfBirth = model.DateOfBirth;
            account.Gender = model.Gender.ToString();

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMassage"] = "Account details updated successfully!";
                return RedirectToAction("Profile", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes. Please try again.");
                return View(model);
            }
        }
        #endregion


        #region Change password
        [HttpGet]
        public IActionResult ChangePassword(int id)
        {
            var user = _context.Accounts.FirstOrDefault(a => a.AccountId == id);

            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Profile", "Account");
            }

            var viewModel = new ChangePasswordViewModel
            {
                AccountId = user.AccountId
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == model.AccountId);

            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Profile", "Account");
            }

            var passwordHasher = new PasswordHasher<Account>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, model.CurrentPassword ?? "");

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("CurrentPassword", "The current password is incorrect.");
                return View(model);
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("ConfirmNewPassword", "The new password and confirmation new password do not match.");
                return View(model);
            }

            user.Password = passwordHasher.HashPassword(user, model.NewPassword ?? "");

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Your password has been changed successfully!";
                return RedirectToAction("Profile", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while changing the password. Please try again.");
                return View(model);
            }
        }
        #endregion

        #region Forgot password
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                ModelState.AddModelError("", "Please enter your email.");
                return View(model);
            }

            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == model.Email && a.IsActive);

            if (user != null)
            {
                var token = await GeneratePasswordResetTokenAsync(user);
                var resetLink = Url.Action("ResetPassword", "Account", new { token }, Request.Scheme);
                await _emailService.SendAsync(user.Email, "Reset Password", $"Click here to reset your password: {resetLink}");
            }

            TempData["SuccessMessage"] = "If the email exists in our system, a reset link has been sent.";
            return RedirectToAction("SuccessSendLinkReset");
        }

        private async Task<string> GeneratePasswordResetTokenAsync(Account user)
        {
            var token = Guid.NewGuid().ToString();
            user.RandomKey = token;
            user.ResetKeyExpires = DateTime.UtcNow.AddMinutes(30);

            await _context.SaveChangesAsync();
            return token;
        }

        [HttpGet]
        public IActionResult ResetPassword(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Invalid token.";
                return RedirectToAction("ForgotPassword");
            }

            var model = new ResetPasswordViewModel { Token = token };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.Accounts.FirstOrDefaultAsync(u => u.RandomKey == model.Token && u.ResetKeyExpires > DateTime.UtcNow);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Invalid or expired token.";
                return RedirectToAction("ForgotPassword");
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("ConfirmNewPassword", "The new password and confirmation new password do not match.");
                return View(model);
            }

            var passwordHasher = new PasswordHasher<Account>();
            user.Password = passwordHasher.HashPassword(user, model.NewPassword ?? "");
            user.RandomKey = null;
            user.ResetKeyExpires = null;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Your password has been reset successfully!";
            return RedirectToAction("Login", "Account");
        }

        public IActionResult SuccessSendLinkReset()
        {
            return View();
        }
        #endregion

        #region Register player
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RegisterPlayer()
        {
            var viewModel = new RegisterPlayerViewModel
            {
                Clubs = await _context.Clubs
                .Where(c => c.IsActive)
                .Select(c => new SelectListItem
                {
                    Value = c.ClubId,
                    Text = c.ClubName
                }).ToListAsync(),

                Positions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Goalkeeper", Text = "Goalkeeper" },
                    new SelectListItem { Value = "Defender", Text = "Defender" },
                    new SelectListItem { Value = "Midfielder", Text = "Midfielder" },
                    new SelectListItem { Value = "Forward", Text = "Forward" }
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPlayer(RegisterPlayerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Clubs = await _context.Clubs
                    .Where(c => c.IsActive)
                    .Select(c => new SelectListItem
                    {
                        Value = c.ClubId,
                        Text = c.ClubName
                    })
                    .ToListAsync();

                model.Positions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Goalkeeper", Text = "Goalkeeper" },
                    new SelectListItem { Value = "Defender", Text = "Defender" },
                    new SelectListItem { Value = "Midfielder", Text = "Midfielder" },
                    new SelectListItem { Value = "Forward", Text = "Forward" }
                };

                return View(model);
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            var registration = new PlayerRegistration
            {
                FirstName = model.FirstName ?? "",
                LastName = model.LastName ?? "",
                Birthday = model.DateOfBirth,
                Position = model.Position ?? "",
                Height = model.Height,
                Number = model.Number,
                Nationality = model.Nationality ?? "",
                UserEmail = userEmail ?? "",
                ClubId = model.ClubId ?? "",
                IsProcessed = false
            };

            if (model.AvatarFile != null && model.AvatarFile.Length > 0)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(model.AvatarFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("AvatarFile", "Only JPEG, PNG and GIF files are allowed.");
                    return View(model);
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(model.AvatarFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("AvatarFile", "Only JPEG, PNG, and GIF files are allowed.");
                    return View(model);
                }

                if (model.AvatarFile.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("AvatarFile", "File size must not exceed 10MB.");
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/player");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{model.AvatarFile.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.AvatarFile.CopyToAsync(fileStream);
                }

                registration.Avatar = $"{uniqueFileName}";
            }

            await _context.PlayerRegistrations.AddAsync(registration);
            await _context.SaveChangesAsync();

            var adminEmail = _config["Smtp:AdminEmail"];

            var subject = "New Player Registration Request";
            var body = $"A new player registration request has been submitted by {userEmail}.\n\n" +
               $"Player Details:\n" +
               $"Name: {model.FirstName} {model.LastName}\n" +
               $"Position: {model.Position}\n" +
               $"Nationality: {model.Nationality}\n\n" +
               "Please review and process this request.";

            await _emailService.SendAsync(adminEmail ?? "", subject, body);

            return RedirectToAction("SuccessRegister");
        }

        public IActionResult SuccessRegister()
        {
            return View();
        }
        #endregion
    }
}
