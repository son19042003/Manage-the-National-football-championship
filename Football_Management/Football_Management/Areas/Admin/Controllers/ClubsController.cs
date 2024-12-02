using Football_Management.Areas.Admin.ViewModels;
using Football_Management.Areas.Admin.ViewModels.Clubs;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClubsController : Controller
    {
        private readonly FootballManagementContext _context;
        public ClubsController(FootballManagementContext context)
        {
            _context = context;
        }

        //GET index
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
        {
            ViewData["ActiveTab"] = "Clubs";

            var clubs = await _context.Clubs
                .OrderByDescending(c => c.IsActive)
                .ThenBy(c => c.ClubId)
                .Select(c => new IndexViewModel
            {
                ClubId = c.ClubId,
                ClubName = c.ClubName,
                LogoUrl = c.Logo ?? string.Empty,
                Stadium = c.Stadium,
                IsActive = c.IsActive
            }).ToListAsync();

            int skip = (pageNumber - 1) * pageSize;
            int totalClubs = await _context.Clubs.CountAsync();

            var paginatedClubs = clubs
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            for (int i = 0; i < clubs.Count; i++)
            {
                clubs[i].Index = i + 1;
            }

            var paginatedResult = new PaginatedViewModel<IndexViewModel>
            {
                Items = paginatedClubs,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalClubs,
            };

            return View(paginatedResult);
        }

        //GET detail
        public async Task<IActionResult> Detail(string id)
        {
            var club = await _context.Clubs
                .Where(c => c.ClubId == id)
                .FirstOrDefaultAsync();

            var totalPlayers = await _context.Players
                .CountAsync(p => p.ClubId == id);

            if(club == null)
            {
                return NotFound();
            }

            var viewModel = new DetailViewModel
            {
                ClubId = club.ClubId,
                ClubName = club.ClubName,
                ShortName = club.ShortName,
                LogoUrl = club.Logo ?? string.Empty,
                Stadium = club.Stadium,
                LinkFb = club.LinkFb,
                LinkIg = club.LinkIg,
                TotalPlayers = totalPlayers,
                IsActive = club.IsActive
            };

            return View(viewModel);
        }

        //create
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var club = new Club
                {
                    ClubId = model.ClubId ?? "",
                    ClubName = model.ClubName ?? "",
                    ShortName = model.ShortName ?? "",
                    Stadium = model.Stadium ?? "",
                    LinkFb = model.LinkFb,
                    LinkIg = model.LinkIg,
                    IsActive = model.IsActive
                };
                
                if (model.LogoFile != null && model.LogoFile.Length > 0)
                {
                    //allow .jpeg, .jpg, .png, .gif
                    var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                    if (!allowedContentTypes.Contains(model.LogoFile.ContentType.ToLower()))
                    {
                        ModelState.AddModelError("LogoFile", "Only JPEG, PNG and GIF files are allowed.");
                        return View(model);
                    }

                    var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                    var fileExtension = Path.GetExtension(model.LogoFile.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("LogoFile", "Only JPEG, PNG, and GIF files are allowed.");
                        return View(model);
                    }

                    //allow picture's size < 10MB
                    if (model.LogoFile.Length > 10 * 1024 * 1024)
                    {
                        ModelState.AddModelError("LogoFile", "File size must not exceed 10MB.");
                    }

                    //upload file
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/logo");

                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    var uniqueFileName = $"{Guid.NewGuid()}_{model.LogoFile.FileName}";
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.LogoFile.CopyToAsync(fileStream);
                    }

                    club.Logo = $"{uniqueFileName}";
                }

                _context.Clubs.Add(club);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        //edit
        //GET
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid Club ID.");
            }

            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            var viewModel = new EditViewModel
            {
                ClubId = club.ClubId,
                ClubName = club.ClubName,
                ShortName = club.ShortName,
                LogoUrl = club.Logo,
                Stadium = club.Stadium,
                LinkFb = club.LinkFb,
                LinkIg = club.LinkIg,
                IsActive = club.IsActive
            };

            return View(viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditViewModel model)
        {
            if (id != model.ClubId)
            {
                return BadRequest("Club ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            if (model.LogoFile != null)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(model.LogoFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("LogoFile", "Only JPEG, PNG and GIF files are allowed.");
                    return View(model);
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(model.LogoFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("LogoFile", "Only JPEG, PNG, and GIF files are allowed.");
                    return View(model);
                }

                if (model.LogoFile.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("LogoFile", "File size must not exceed 10MB.");
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/logo");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{model.LogoFile.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.LogoFile.CopyToAsync(fileStream);
                }

                club.Logo = $"{uniqueFileName}";
            }

            club.ClubName = model.ClubName ?? "";
            club.ShortName = model.ShortName ?? "";
            club.Stadium = model.Stadium ?? "";
            club.LinkFb = model.LinkFb;
            club.LinkIg = model.LinkIg;
            club.IsActive = model.IsActive;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMassage"] = "Club details updated successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes. Please try again.");
                return View(model);
            }
        }

        //Delete
        //GET
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var club = await _context.Clubs
                .Where(c => c.ClubId == id)
                .FirstOrDefaultAsync();

            if (club == null)
            {
                return NotFound();
            }

            var viewModel = new DeleteViewModel
            {
                ClubId = club.ClubId,
                ClubName = club.ClubName,
                ShortName = club.ShortName,
                Stadium = club.Stadium,
                LogoUrl = club.Logo,
                LinkFb = club.LinkFb,
                LinkIg = club.LinkIg,
                IsActive = club.IsActive,
                ConfirmDelete = false
            };

            return View(viewModel);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Delete(string id, DeleteViewModel model)
        {
            var club = await _context.Clubs
                .Where(c => c.ClubId == id)
                .FirstOrDefaultAsync();

            if (club == null)
            {
                return NotFound();
            }

            if (model.ConfirmDelete)
            {
                _context.Clubs.Remove(club);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete");
            }

            //return View(model);
        }
    }
}
