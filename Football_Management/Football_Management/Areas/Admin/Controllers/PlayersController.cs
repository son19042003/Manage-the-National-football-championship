using Football_Management.Areas.Admin.ViewModels;
using Football_Management.Areas.Admin.ViewModels.Players;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlayersController : Controller
    {
        private readonly FootballManagementContext _context;

        public PlayersController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
        {
            ViewData["ActiveTab"] = "Players";

            var players = await _context.Players
                .Include(p => p.Club)
                .OrderBy(p => p.ClubId)
                .ThenByDescending(p => p.IsInClub)
                .ThenBy(p => p.FirstName)
                .Select(p => new IndexViewModel
                {
                    PlayerId = p.PlayerId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    IsInClub = p.IsInClub,
                    ClubName = p.Club.ClubName
                })
                .ToListAsync();

            int skip = (pageNumber - 1) * pageSize;
            int totalPlayers = await _context.Players.CountAsync();

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
                TotalItems = totalPlayers
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
        public async Task<IActionResult> Create()
        {
            ViewData["ActiveTab"] = "Players";

            var viewModel = new CreateViewModel
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

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
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
            await _context.SaveChangesAsync();

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
