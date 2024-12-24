using Football_Management.Areas.Admin.ViewModels.NewsTypes;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsTypesController : Controller
    {
        private readonly FootballManagementContext _context;
        public NewsTypesController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ActiveTab"] = "NewsTypes";
            
            var typeNews = _context.TypeNews
                .Select(tn => new IndexViewModel
                {
                    TypeNewsId = tn.TypeNewsId,
                    TypeNewsName = tn.TypeNewsName,
                    TypeNewsDescription = tn.TypeNewsDes
                }).ToList();

            for (int i = 0; i < typeNews.Count; i++)
            {
                typeNews[i].Index = i + 1;
            }

            return View(typeNews);
        }


        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["ActiveTab"] = "NewsTypes";

            var newsType = await _context.TypeNews
                .FirstOrDefaultAsync(nt => nt.TypeNewsId == id);

            if (newsType == null)
            {
                return NotFound();
            }

            var viewModel = new EditViewModel
            {
                TypeNewsId = newsType.TypeNewsId,
                TypeNewsName = newsType.TypeNewsName,
                TypeNewsDescription = newsType.TypeNewsDes
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var newsType = await _context.TypeNews
                .FindAsync(model.TypeNewsId);

            if (newsType == null)
            {
                return NotFound();
            }

            newsType.TypeNewsName = model.TypeNewsName ?? "";
            newsType.TypeNewsDes = model.TypeNewsDescription;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Type news updated successfully!";
                return RedirectToAction("Index", new { id = model.TypeNewsId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while saving changes: {ex.Message}");
            }

            return View(model);
        }


        //Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ActiveTab"] = "NewsTypes";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            var typeNews = new TypeNews
            {
                TypeNewsId = model.TypeNewsId,
                TypeNewsName = model.TypeNewsName ?? "",
                TypeNewsDes = model.TypeNewsDescription
            };

            _context.TypeNews.Add(typeNews);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        //Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewData["ActiveTab"] = "NewsTypes";

            var typeNews = _context.TypeNews.FirstOrDefault(tn => tn.TypeNewsId == id);

            if (typeNews == null)
            {
                return NotFound();
            }

            var viewModel = new DeleteViewModel
            {
                TypeNewsId = typeNews.TypeNewsId,
                TypeNewsName = typeNews.TypeNewsName,
                TypeNewsDescription = typeNews.TypeNewsDes,
                ConfirmDelete = false
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteViewModel model)
        {
            var typeNews = await _context.TypeNews.FirstOrDefaultAsync(tn => tn.TypeNewsId == id);

            if (typeNews == null)
            {
                return NotFound();
            }

            if (model.ConfirmDelete)
            {
                _context.TypeNews.Remove(typeNews);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete");
            }
        }
    }
}
