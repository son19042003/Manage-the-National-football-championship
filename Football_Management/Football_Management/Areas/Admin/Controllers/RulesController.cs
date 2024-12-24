using Football_Management.Areas.Admin.ViewModels.Rules;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RulesController : Controller
    {
        private readonly FootballManagementContext _context;
        public RulesController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ActiveTab"] = "Rules";

            var rules = _context.Rules
                .Select(r => new IndexViewModel
                {
                    RuleId = r.RuleId,
                    MinAge = r.MinAge,
                    MaxForeignPlayers = r.MaxForeignPlayers,
                    MinPlayer = r.MinPlayer,
                    MaxPlayer = r.MaxPlayer
                }).ToList();

            return View(rules);
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["ActiveTab"] = "Rules";

            var rule = _context.Rules
                .FirstOrDefault(r => r.RuleId == id);

            if (rule == null)
            {
                return NotFound();
            }

            var viewModel = new EditViewModel
            {
                RuleId = rule.RuleId,
                MinAge = rule.MinAge,
                MaxForeignPlayers = rule.MaxForeignPlayers,
                MinPlayer = rule.MinPlayer,
                MaxPlayer = rule.MaxPlayer
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (model.MinPlayer > model.MaxPlayer)
            {
                ModelState.AddModelError("MinPlayer", "Minimum players cannot be greater than Maximum players.");
            }

            if (model.MaxPlayer < model.MinPlayer)
            {
                ModelState.AddModelError("MaxPlayer", "Maximum players cannot be less than Minimum players.");
            }

            if (model.MaxForeignPlayers > model.MaxPlayer)
            {
                ModelState.AddModelError("MaxForeignPlayers", "Maximum foreign players cannot be greater than Maximum players.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var rule = await _context.Rules
                .FindAsync(model.RuleId);

            if (rule == null)
            {
                return NotFound();
            }

            rule.MinAge = model.MinAge;
            rule.MinPlayer = model.MinPlayer;
            rule.MaxPlayer = model.MaxPlayer;
            rule.MaxForeignPlayers = model.MaxForeignPlayers;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Rule updated successfully!";
                return RedirectToAction("Index", new { id = model.RuleId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while saving changes: {ex.Message}");
            }

            return View(model);
        }
    }
}
