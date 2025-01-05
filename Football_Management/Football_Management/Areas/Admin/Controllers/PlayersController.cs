using Football_Management.Areas.Admin.ViewModels;
using Football_Management.Areas.Admin.ViewModels.Players;
using Football_Management.Models;
using Football_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PlayersController : Controller
    {
        private readonly FootballManagementContext _context;
        private readonly IEmailService _emailService;

        public PlayersController(FootballManagementContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20, string clubId = "0")
        {
            ViewData["ActiveTab"] = "Players";

            var clubs = await _context.Clubs
                .Where(c => c.IsActive)
                .Select(c => new ClubViewModel
                {
                    ClubId = c.ClubId,
                    ClubName = c.ClubName
                })
                .ToListAsync();
            clubs.Insert(0, new ClubViewModel { ClubId = "0", ClubName = "All" });

            var playersQuery = _context.Players.Include(p => p.Club).AsQueryable();

            if (!string.IsNullOrEmpty(clubId) && clubId != "0")
            {
                playersQuery = playersQuery.Where(p => p.ClubId.ToString() == clubId);
            }

            var players = await playersQuery
                .OrderBy(p => p.ClubId)
                .ThenByDescending(p => p.IsInClub)
                .ThenBy(p => p.FirstName)
                .Select(p => new IndexViewModel
                {
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    IsInClub = p.IsInClub,
                    ClubName = p.Club.ClubName,
                    ClubId = p.ClubId
                })
                .ToListAsync();

            int skip = (pageNumber - 1) * pageSize;
            var totalPlayers = await playersQuery.CountAsync();

            var paginatedPlayers = players
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            for (int i = 0; i < players.Count; i++)
            {
                players[i].Index = i + 1;
            }

            var paginatedResult = new PaginatedViewModel<IndexViewModel>
            {
                Items = paginatedPlayers,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalPlayers,
                Clubs = clubs,
                ClubId = clubId
            };

            return View(paginatedResult);
        }


        //Detail
        [HttpGet]
        public async Task<IActionResult> Detail(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "Players";

            var viewModel = await _context.Players
                .Include(p => p.Club)
                .Where(p => p.PlayerId == id)
                .Select(player => new DetailViewModel
                {
                    PlayerId = player.PlayerId,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    AvatarUrl = player.Avatar,
                    Birthday = player.Birthday,
                    Height = player.Height,
                    Nationality = player.Nationality,
                    DomesticPlayer = player.TypePlayer ?? false,
                    Position = player.Position,
                    Number = player.Number,
                    ClubName = player.Club.ClubName,
                    LinkFb = player.LinkFb,
                    LinkIg = player.LinkIg,
                    IsInClub = player.IsInClub,
                    Goals = player.Goals ?? 0
                })
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return NotFound();
            }

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }


        //Edit
        //GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "Players";

            var player = await _context.Players
                .Include(p => p.Club)
                .FirstOrDefaultAsync(p => p.PlayerId == id);

            if (player == null)
            {
                return NotFound();
            }

            var clubs = _context.Clubs
                .Where(c => c.IsActive)
                .Select(c => new SelectListItem
                {
                    Value = c.ClubId,
                    Text = c.ClubName
                })
                .ToList();

            var positions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Goalkeeper", Text = "Goalkeeper" },
                new SelectListItem { Value = "Defender", Text = "Defender" },
                new SelectListItem { Value = "Midfielder", Text = "Midfielder" },
                new SelectListItem { Value = "Forward", Text = "Forward" }
            };

            var viewModel = new EditViewModel
            {
                PlayerId = player.PlayerId,
                FirstName = player.FirstName,
                LastName = player.LastName,
                AvatarUrl = player.Avatar,
                Birthday = player.Birthday,
                Height = player.Height,
                Position = player.Position,
                Positions = positions,
                Number = player.Number,
                Nationality = player.Nationality,
                DomesticPlayer = string.Equals(player.Nationality, "England", StringComparison.OrdinalIgnoreCase),
                LinkFb = player.LinkFb,
                LinkIg = player.LinkIg,
                ClubId = player.ClubId,
                Clubs = clubs,
                IsInClub = player.IsInClub
            };

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Clubs = await _context.Clubs
                    .Where(c => c.IsActive)
                    .Select(c => new SelectListItem
                    {
                        Value = c.ClubId,
                        Text = c.ClubName
                    })
                    .ToListAsync();

                viewModel.Positions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Goalkeeper", Text = "Goalkeeper" },
                    new SelectListItem { Value = "Defender", Text = "Defender" },
                    new SelectListItem { Value = "Midfielder", Text = "Midfielder" },
                    new SelectListItem { Value = "Forward", Text = "Forward" }
                };

                return View(viewModel);
            }

            var isShirtNumberTaken = await _context.Players.AnyAsync(p =>
                p.ClubId == viewModel.ClubId &&
                p.Number == viewModel.Number &&
                p.PlayerId != viewModel.PlayerId);

            if (isShirtNumberTaken)
            {
                ModelState.AddModelError("Number", "The shirt number is already taken by another player in this club.");

                viewModel.Clubs = await _context.Clubs
                    .Where(c => c.IsActive)
                    .Select(c => new SelectListItem
                    {
                        Value = c.ClubId,
                        Text = c.ClubName
                    })
                    .ToListAsync();

                viewModel.Positions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Goalkeeper", Text = "Goalkeeper" },
                    new SelectListItem { Value = "Defender", Text = "Defender" },
                    new SelectListItem { Value = "Midfielder", Text = "Midfielder" },
                    new SelectListItem { Value = "Forward", Text = "Forward" }
                };
                return View(viewModel);
            }

            var player = await _context.Players
                .Include(p => p.Club)
                .FirstOrDefaultAsync(p => p.PlayerId == viewModel.PlayerId);

            if (player == null)
            {
                return NotFound();
            }

            if (viewModel.AvatarFile != null)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(viewModel.AvatarFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("AvatarFile", "Only JPEG, PNG and GIF files are allowed.");
                    return View(viewModel);
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(viewModel.AvatarFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("AvatarFile", "Only JPEG, PNG, and GIF files are allowed.");
                    return View(viewModel);
                }

                if (viewModel.AvatarFile.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("AvatarFile", "File size must not exceed 10MB.");
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/player");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{viewModel.AvatarFile.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.AvatarFile.CopyToAsync(fileStream);
                }

                player.Avatar = $"{uniqueFileName}";
            }

            player.FirstName = viewModel.FirstName ?? "";
            player.LastName = viewModel.LastName ?? "";
            player.Birthday = viewModel.Birthday;
            player.Height = viewModel.Height;
            player.Position = viewModel.Position ?? "";
            player.Number = viewModel.Number;
            player.Nationality = viewModel.Nationality ?? "";
            player.LinkFb = viewModel.LinkFb;
            player.LinkIg = viewModel.LinkIg;
            player.ClubId = viewModel.ClubId ?? "";
            player.IsInClub = viewModel.IsInClub;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMassage"] = "Player details updated successfully!";

                int pageNumber = viewModel.PageNumber;
                return RedirectToAction(nameof(Index), new { pageNumber });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes. Please try again.");
                return View(viewModel);
            }
        }


        //Create
        //GET
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            ViewData["ActiveTab"] = "Players";

            var registration = await _context.PlayerRegistrations.FindAsync(id);

            if (registration == null)
            {
                return NotFound();
            }

            var clubs = await _context.Clubs
                .Where(c => c.IsActive)
                .Select(c => new SelectListItem
                {
                    Value = c.ClubId,
                    Text = c.ClubName
                }).ToListAsync();

            var positions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Goalkeeper", Text = "Goalkeeper" },
                new SelectListItem { Value = "Defender", Text = "Defender" },
                new SelectListItem { Value = "Midfielder", Text = "Midfielder" },
                new SelectListItem { Value = "Forward", Text = "Forward" }
            };

            var viewModel = new CreateViewModel
            {
                FirstName = registration.FirstName,
                LastName = registration.LastName,
                Birthday = registration.Birthday,
                Height = registration.Height,
                Position = registration.Position,
                Positions = positions,
                Number = registration.Number,
                Nationality = registration.Nationality,
                LinkFb = registration.LinkFb,
                LinkIg = registration.LinkIg,
                AvatarUrl = registration.Avatar,
                ClubId = registration.ClubId,
                Clubs = clubs,
                RegistrationId = registration.PlayerRegisId
            };

            return View(viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel viewModel, int registrationId)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Clubs = await _context.Clubs
                    .Where(c => c.IsActive)
                    .Select(c => new SelectListItem
                    {
                        Value = c.ClubId,
                        Text = c.ClubName
                    })
                    .ToListAsync();

                viewModel.Positions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Goalkeeper", Text = "Goalkeeper" },
                    new SelectListItem { Value = "Defender", Text = "Defender" },
                    new SelectListItem { Value = "Midfielder", Text = "Midfielder" },
                    new SelectListItem { Value = "Forward", Text = "Forward" }
                };

                return View(viewModel);
            }

            var player = new Player
            {
                FirstName = viewModel.FirstName ?? "",
                LastName = viewModel.LastName ?? "",
                Birthday = viewModel.Birthday,
                Height = viewModel.Height,
                Position = viewModel.Position ?? "",
                Number = viewModel.Number,
                Nationality = viewModel.Nationality ?? "",
                TypePlayer = string.Equals(viewModel.Nationality, "England", StringComparison.OrdinalIgnoreCase),
                LinkFb = viewModel.LinkFb,
                LinkIg = viewModel.LinkIg,
                ClubId = viewModel.ClubId ?? "",
                IsInClub = viewModel.IsInClub
            };

            var isShirtNumberTaken = await _context.Players.AnyAsync(p =>
                p.ClubId == viewModel.ClubId &&
                p.Number == viewModel.Number);

            var registration = _context.PlayerRegistrations.Find(registrationId);
            var userEmail = registration?.UserEmail;

            if (isShirtNumberTaken)
            {
                ModelState.AddModelError("Number", "The shirt number is already taken by another player in this club.");

                var subjectErr = "Player Registration Refused";
                var bodyErr = $"Hello,\n\nYour player registration for {player.FirstName} {player.LastName} has been refused becasue shirt number is already taken by another player in this club.\n\nWe're sorry!";

                await _emailService.SendAsync(userEmail ?? "", subjectErr, bodyErr);

                viewModel.Clubs = await _context.Clubs
                    .Where(c => c.IsActive)
                    .Select(c => new SelectListItem
                    {
                        Value = c.ClubId,
                        Text = c.ClubName
                    })
                    .ToListAsync();

                viewModel.Positions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Goalkeeper", Text = "Goalkeeper" },
                    new SelectListItem { Value = "Defender", Text = "Defender" },
                    new SelectListItem { Value = "Midfielder", Text = "Midfielder" },
                    new SelectListItem { Value = "Forward", Text = "Forward" }
                };
                return View(viewModel);
            }

            var countForeignPlayers = await _context.Players
                .Where(p => p.ClubId == player.ClubId)
                .CountAsync(p => p.TypePlayer == false);

            var rule = await _context.Rules.FirstOrDefaultAsync();
            if (rule != null && rule.MaxForeignPlayers <= countForeignPlayers)
            {
                ModelState.AddModelError("", "The maximum number of foreign players has been reached.");

                var subjectErr = "Player Registration Refused";
                var bodyErr = $"Hello,\n\nYour player registration for {player.FirstName} {player.LastName} has been refused becasue the maximum number of foreign players of your club has been reached..\n\nWe're sorry!";

                await _emailService.SendAsync(userEmail ?? "", subjectErr, bodyErr);

                viewModel.Clubs = await _context.Clubs
                    .Where(c => c.IsActive)
                    .Select(c => new SelectListItem
                    {
                        Value = c.ClubId,
                        Text = c.ClubName
                    })
                    .ToListAsync();

                viewModel.Positions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Goalkeeper", Text = "Goalkeeper" },
                    new SelectListItem { Value = "Defender", Text = "Defender" },
                    new SelectListItem { Value = "Midfielder", Text = "Midfielder" },
                    new SelectListItem { Value = "Forward", Text = "Forward" }
                };
                return View(viewModel);
            }

            if (viewModel.AvatarFile != null && viewModel.AvatarFile.Length > 0)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(viewModel.AvatarFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("AvatarFile", "Only JPEG, PNG and GIF files are allowed.");
                    return View(viewModel);
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(viewModel.AvatarFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("AvatarFile", "Only JPEG, PNG, and GIF files are allowed.");
                    return View(viewModel);
                }

                if (viewModel.AvatarFile.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("AvatarFile", "File size must not exceed 10MB.");
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/player");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{viewModel.AvatarFile.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.AvatarFile.CopyToAsync(fileStream);
                }

                player.Avatar = $"{uniqueFileName}";
            }
            else
            {
                player.Avatar = "default.png";
            }

            _context.Players.Add(player);

            if (registration != null)
            {
                registration.IsProcessed = true;
            }

            await _context.SaveChangesAsync();

            var subject = "Player Registration Approved";
            var body = $"Hello,\n\nYour player registration for {player.FirstName} {player.LastName} has been approved and added to our system.\n\nThank you!";

            await _emailService.SendAsync(userEmail ?? "", subject, body);

            return RedirectToAction("Index");
        }


        //Delete
        //GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "Players";

            var player = await _context.Players
                .Include(p => p.Club)
                .Where(p => p.PlayerId == id)
                .FirstOrDefaultAsync();

            if (player == null)
            {
                return NotFound();
            }

            var viewModel = new DeleteViewModel
            {
                PlayerId = player.PlayerId,
                FirstName = player.FirstName,
                LastName = player.LastName,
                AvatarUrl = player.Avatar,
                Birthday = player.Birthday,
                Height = player.Height,
                Nationality = player.Nationality,
                DomesticPlayer = player.TypePlayer ?? false,
                Position = player.Position,
                Number = player.Number,
                ClubName = player.Club.ClubName,
                LinkFb = player.LinkFb,
                LinkIg = player.LinkIg,
                IsInClub = player.IsInClub,
                Goals = player.Goals ?? 0,
                ConfirmDelete = false
            };

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteViewModel viewModel)
        {
            var player = await _context.Players
                .Where(p => p.PlayerId == id)
                .FirstOrDefaultAsync();

            if(player == null)
            {
                return NotFound();
            }

            if (viewModel.ConfirmDelete)
            {
                _context.Players.Remove(player);
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
