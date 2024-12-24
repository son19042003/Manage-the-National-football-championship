using Football_Management.Areas.Admin.ViewModels;
using Football_Management.Areas.Admin.ViewModels.Accounts;
using Football_Management.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        private readonly FootballManagementContext _context;
        public AccountsController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 20)
        {
            ViewData["ActiveTab"] = "Accounts";

            var accounts = _context.Accounts
                .Include(a => a.Role)
                .OrderByDescending(a => a.IsActive)
                .ThenBy(a => a.RoleId)
                .Select(a => new IndexViewModel
                {
                    AccountId = a.AccountId,
                    UserName = a.Username,
                    RoleName = a.Role.RoleName,
                    IsActive = a.IsActive
                }).ToList();

            int skip = (pageNumber - 1) * pageSize;
            int totalPlayers = _context.Accounts.Count();

            var paginatedAccounts = accounts
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            for (int i = 0; i < accounts.Count; i++)
            {
                accounts[i].Index = i + 1;
            }

            var paginatedResult = new PaginatedViewModel<IndexViewModel>
            {
                Items = paginatedAccounts,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalPlayers
            };

            return View(paginatedResult);
        }


        //Detail
        [HttpGet]
        public IActionResult Detail(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "Accounts";

            var viewModel = _context.Accounts
                .Include(a => a.Role)
                .Where(a => a.AccountId == id)
                .Select(a => new DetailViewModel
                {
                    AccountsId = a.AccountId,
                    UserName = a.Username,
                    Password = a.Password,
                    Email = a.Email,
                    PhoneNumber = a.PhoneNum,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Gender = a.Gender,
                    DateOfBirth = a.DateOfBirth,
                    AvatarUrl = a.Avatar,
                    RoleName = a.Role.RoleName,
                    IsActive = a.IsActive
                })
                .FirstOrDefault();

            if (viewModel == null)
            {
                return NotFound();
            }

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }


        //Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ActiveTab"] = "Accounts";

            var viewModel = new CreateViewModel
            {
                ListRoles = _context.Roles
                .Select(lr => new SelectListItem
                {
                    Value = lr.RoleId.ToString(),
                    Text = lr.RoleName
                }).ToList(),

                GenderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Unspecified", Text = "Unspecified" }
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ListRoles = await _context.Roles
                    .Select(lr => new SelectListItem
                    {
                        Value = lr.RoleId.ToString(),
                        Text = lr.RoleName
                    }).ToListAsync();

                model.GenderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Unspecified", Text = "Unspecified" }
                };

                return View(model);
            }

            var checkUsername = await _context.Accounts
                .AnyAsync(a => a.Username == model.UserName);

            if (checkUsername)
            {
                ModelState.AddModelError("UserName", "Username already exists.");

                model.ListRoles = await _context.Roles
                    .Select(lr => new SelectListItem
                    {
                        Value = lr.RoleId.ToString(),
                        Text = lr.RoleName
                    }).ToListAsync();

                model.GenderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Unspecified", Text = "Unspecified" }
                };

                return View(model);
            }

            var checkEmail = await _context.Accounts
                .AnyAsync(a => a.Email == model.Email);

            if (checkEmail)
            {
                ModelState.AddModelError("Email", "Email already exists.");

                model.ListRoles = await _context.Roles
                    .Select(lr => new SelectListItem
                    {
                        Value = lr.RoleId.ToString(),
                        Text = lr.RoleName
                    }).ToListAsync();

                model.GenderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Unspecified", Text = "Unspecified" }
                };

                return View(model);
            }

            var passwordHasher = new PasswordHasher<Account>();

            var account = new Account
            {
                Username = model.UserName ?? "",
                Password = passwordHasher.HashPassword(null, model.Password ?? ""),
                Email = model.Email ?? "",
                PhoneNum = model.PhoneNumber ?? "",
                FirstName = model.FirstName ?? "",
                LastName = model.LastName ?? "",
                Gender = model.Gender ?? "Unspecified",
                DateOfBirth =  model.DateOfBirth,
                RoleId = model.RoleId,
                IsActive = model.IsActive
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
            return RedirectToAction("Index");
        }


        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "Accounts";

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            var listRoles = await _context.Roles
                .Select(lr => new SelectListItem
                {
                    Value = lr.RoleId.ToString(),
                    Text = lr.RoleName
                })
                .ToListAsync();

            var viewModel = new EditViewModel
            {
                AccountsId = account.AccountId,
                UserName = account.Username,
                Email = account.Email,
                PhoneNumber = account.PhoneNum,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Gender = account.Gender,
                GenderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Unspecified", Text = "Unspecified" }
                },
                DateOfBirth = account.DateOfBirth,
                AvatarUrl = account.Avatar,
                RoleId = account.RoleId,
                ListRoles = listRoles,
                IsActive = account.IsActive
            };

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ListRoles = await _context.Roles
                    .Select(lr => new SelectListItem
                    {
                        Value = lr.RoleId.ToString(),
                        Text = lr.RoleName
                    }).ToListAsync();

                model.GenderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Unspecified", Text = "Unspecified" }
                };

                return View(model);
            }

            var checkUsername = await _context.Accounts
                .AnyAsync(a => a.Username == model.UserName && a.AccountId != model.AccountsId);

            if (checkUsername)
            {
                ModelState.AddModelError("UserName", "Username already exists.");

                model.ListRoles = await _context.Roles
                    .Select(lr => new SelectListItem
                    {
                        Value = lr.RoleId.ToString(),
                        Text = lr.RoleName
                    }).ToListAsync();

                model.GenderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Unspecified", Text = "Unspecified" }
                };

                return View(model);
            }

            var checkEmail = await _context.Accounts
                .AnyAsync(a => a.Email == model.Email && a.AccountId != model.AccountsId);

            if (checkEmail)
            {
                ModelState.AddModelError("Email", "Email already exists.");

                model.ListRoles = await _context.Roles
                    .Select(lr => new SelectListItem
                    {
                        Value = lr.RoleId.ToString(),
                        Text = lr.RoleName
                    }).ToListAsync();

                model.GenderOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Male", Text = "Male" },
                    new SelectListItem { Value = "Female", Text = "Female" },
                    new SelectListItem { Value = "Unspecified", Text = "Unspecified" }
                };

                return View(model);
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.AccountId == model.AccountsId);

            if (account == null)
            {
                return NotFound();
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

            account.Username = model.UserName ?? "";
            account.Email = model.Email ?? "";
            account.PhoneNum = model.PhoneNumber ?? "";
            account.FirstName = model.FirstName ?? "";
            account.LastName = model.LastName ?? "";
            account.Gender = model.Gender ?? "Unspecified";
            account.DateOfBirth = model.DateOfBirth;
            account.RoleId = model.RoleId;
            account.IsActive = model.IsActive;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMassage"] = "Account details updated successfully!";

                int pageNumber = model.PageNumber;
                return RedirectToAction(nameof(Index), new { pageNumber });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes. Please try again.");
                return View(model);
            }
        }


        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "Accounts";

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            var viewModel = new DeleteViewModel
            {
                AccountsId = account.AccountId,
                UserName = account.Username,
                Email = account.Email,
                PhoneNumber = account.PhoneNum,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Gender = account.Gender,
                DateOfBirth = account.DateOfBirth,
                AvatarUrl = account.Avatar,
                RoleName = account.Role.RoleName,
                IsActive = account.IsActive,
                ConfirmDelete = false
            };

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteViewModel viewModel)
        {
            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(a => a.AccountId == id);

            if (account == null)
            {
                return NotFound();
            }

            if (viewModel.ConfirmDelete)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();

                int pageNumber = TempData["PageNumber"] != null ? Convert.ToInt32(TempData["PageNumber"]) : 1;
                return RedirectToAction(nameof(Index), new { pageNumber });
            }
            else
            {
                return RedirectToAction("Delete");
            }
        }
    }
}
