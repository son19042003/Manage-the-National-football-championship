using Football_Management.Areas.Admin.ViewModels;
using Football_Management.Areas.Admin.ViewModels.NewsPages;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Numerics;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsPagesController : Controller
    {
        private readonly FootballManagementContext _context;
        public NewsPagesController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            ViewData["ActiveTab"] = "NewsPages";

            var news = _context.News
                .Include(n => n.TypeNews)
                .Select(n => new IndexViewModel
                {
                    NewsId = n.NewsId,
                    Title = n.Title,
                    TypeNewsName = n.TypeNews.TypeNewsName,
                    Status = n.Status
                }).ToList();

            int skip = (pageNumber - 1) * pageSize;
            int totalNews = _context.News.Count();

            var paginatedNews = news
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            for (int i = 0; i < news.Count; i++)
            {
                news[i].Index = i + 1;
            }

            var paginatedResult = new PaginatedViewModel<IndexViewModel>
            {
                Items = paginatedNews,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalNews
            };

            return View(paginatedResult);
        }


        //Detail
        [HttpGet]
        public IActionResult Detail (int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "NewsPages";

            var news = _context.News
                .Include(n => n.TypeNews)
                .Where(p => p.NewsId == id)
                .FirstOrDefault();

            if (news == null)
            {
                return NotFound();
            }

            var viewModel = new DetailViewModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                DateUpdated = news.DateU,
                ImageUrl = news.Image,
                ImgContentUrl = news.ImgContent,
                Content = news.Content,
                TypeNewsName = news.TypeNews.TypeNewsName,
                Status = news.Status
            };

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }


        //Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ActiveTab"] = "NewsPages";

            var viewModel = new CreateViewModel
            {
                ListTypeNews = _context.TypeNews
                .Select(ltn => new SelectListItem
                {
                    Value = ltn.TypeNewsId.ToString(),
                    Text = ltn.TypeNewsName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.ListTypeNews = await _context.TypeNews
                    .Select(ltn => new SelectListItem
                    {
                        Value = ltn.TypeNewsId.ToString(),
                        Text = ltn.TypeNewsName
                    }).ToListAsync();

                return View(viewModel);
            }

            viewModel.DateUpdated = DateTime.Now;

            var news = new News
            {
                Title = viewModel.Title ?? "",
                DateU = viewModel.DateUpdated,
                TypeNewsId = viewModel.TypeNewsId,
                Content = viewModel.Content ?? "",
                Status = viewModel.Status,
                Image = viewModel.ImageUrl ?? "",
                ImgContent = viewModel.ImgContentUrl
            };

            //thumbnail
            if (viewModel.ThumbFile != null && viewModel.ThumbFile.Length > 0)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(viewModel.ThumbFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("ThumbFile", "Only JPEG, PNG and GIF files are allowed.");
                    return View(viewModel);
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(viewModel.ThumbFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ThumbFile", "Only JPEG, PNG, and GIF files are allowed.");
                    return View(viewModel);
                }

                if (viewModel.ThumbFile.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("ThumbFile", "File size must not exceed 10MB.");
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/news/thumbnail");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{viewModel.ThumbFile.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.ThumbFile.CopyToAsync(fileStream);
                }

                news.Image = $"{uniqueFileName}";
            }
            else
            {
                news.Image = "default.jpg";
            }

            //image content
            if (viewModel.ContentFile != null && viewModel.ContentFile.Length > 0)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(viewModel.ContentFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("ContentFile", "Only JPEG, PNG and GIF files are allowed.");
                    return View(viewModel);
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(viewModel.ContentFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ContentFile", "Only JPEG, PNG, and GIF files are allowed.");
                    return View(viewModel);
                }

                if (viewModel.ContentFile.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("ContentFile", "File size must not exceed 10MB.");
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/news/content");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{viewModel.ContentFile.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await viewModel.ContentFile.CopyToAsync(fileStream);
                }

                news.ImgContent = $"{uniqueFileName}";
            }

            _context.News.Add(news);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "NewsPages";

            var news = _context.News
                .Include(n => n.TypeNews)
                .FirstOrDefault(n => n.NewsId == id);

            if (news == null)
            {
                return NotFound();
            }

            var typeNews = _context.TypeNews
                .Select(tn => new SelectListItem
                {
                    Value = tn.TypeNewsId.ToString(),
                    Text = tn.TypeNewsName
                }).ToList();

            var viewModel = new EditViewModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                DateUpdated = news.DateU,
                ImageUrl = news.Image,
                ImgContentUrl = news.ImgContent,
                Content = news.Content,
                TypeNewsId = news.TypeNewsId,
                ListTypeNews = typeNews,
                Status = news.Status
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
                var typeNews = await _context.TypeNews
                .Select(tn => new SelectListItem
                {
                    Value = tn.TypeNewsId.ToString(),
                    Text = tn.TypeNewsName
                }).ToListAsync();

                return View(model);
            }

            var news = await _context.News
                .Include(n => n.TypeNews)
                .FirstOrDefaultAsync(n => n.NewsId == model.NewsId);

            if (news == null)
            {
                return NotFound();
            }

            //thumbnail
            if (model.ThumbFile != null && model.ThumbFile.Length > 0)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(model.ThumbFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("ThumbFile", "Only JPEG, PNG and GIF files are allowed.");
                    return View(model);
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(model.ThumbFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ThumbFile", "Only JPEG, PNG, and GIF files are allowed.");
                    return View(model);
                }

                if (model.ThumbFile.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("ThumbFile", "File size must not exceed 10MB.");
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/news/thumbnail");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{model.ThumbFile.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ThumbFile.CopyToAsync(fileStream);
                }

                news.Image = $"{uniqueFileName}";
            }
            else
            {
                news.Image = "default.jpg";
            }

            //image content
            if (model.ContentFile != null && model.ContentFile.Length > 0)
            {
                var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                if (!allowedContentTypes.Contains(model.ContentFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("ContentFile", "Only JPEG, PNG and GIF files are allowed.");
                    return View(model);
                }

                var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".gif" };
                var fileExtension = Path.GetExtension(model.ContentFile.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("ContentFile", "Only JPEG, PNG, and GIF files are allowed.");
                    return View(model);
                }

                if (model.ContentFile.Length > 10 * 1024 * 1024)
                {
                    ModelState.AddModelError("ContentFile", "File size must not exceed 10MB.");
                }

                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assets/images/news/content");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{model.ContentFile.FileName}";
                var filePath = Path.Combine(uploadFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ContentFile.CopyToAsync(fileStream);
                }

                news.ImgContent = $"{uniqueFileName}";
            }

            model.DateUpdated = DateTime.Now;

            news.Title = model.Title ?? "";
            news.DateU = model.DateUpdated;
            news.Content = model.Content ?? "";
            news.TypeNewsId = model.TypeNewsId;
            news.Status = model.Status;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMassage"] = "News details updated successfully!";

                int pageNumber = TempData["PageNumber"] != null ? Convert.ToInt32(TempData["PageNumber"]) : 1;
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
        public IActionResult Delete(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "NewsPages";

            var news = _context.News
                .Include(n => n.TypeNews)
                .Where(n => n.NewsId == id)
                .FirstOrDefault();

            if (news == null)
            {
                return NotFound();
            }

            var viewModel = new DeleteViewModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                DateUpdated = news.DateU,
                ImageUrl = news.Image,
                ImgContentUrl = news.ImgContent,
                Content = news.Content,
                TypeNewsName = news.TypeNews.TypeNewsName,
                Status = news.Status,
                ConfirmDelete = false
            };

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteViewModel viewModel)
        {
            var news = await _context.News
                .Where(n => n.NewsId == id)
                .FirstOrDefaultAsync();

            if (news == null)
            {
                return NotFound();
            }

            if (viewModel.ConfirmDelete)
            {
                _context.News.Remove(news);
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
