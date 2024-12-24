using Football_Management.Areas.Admin.ViewModels.TypeGoals;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TypeGoalsController : Controller
    {
        private readonly FootballManagementContext _context;
        public TypeGoalsController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ActiveTab"] = "GoalTypes";

            var goaltype = _context.TypeGoals
                .Select(gt => new IndexViewModel
                {
                    TypeGoalId = gt.TypeGid,
                    TypeGoalName = gt.TypeGname,
                    TypeGoalDescription = gt.TypeGdes
                }).ToList();

            for (int i = 0; i < goaltype.Count; i++)
            {
                goaltype[i].Index = i + 1;
            }

            return View(goaltype);
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["ActiveTab"] = "GoalTypes";

            var goaltype = _context.TypeGoals.FirstOrDefault(gt => gt.TypeGid == id);

            if (goaltype == null)
            {
                return NotFound();
            }

            var viewModel = new EditViewModel
            {
                TypeGoalId = goaltype.TypeGid,
                TypeGoalName = goaltype.TypeGname,
                TypeGoalDescription= goaltype.TypeGdes
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var goaltype = await _context.TypeGoals.FindAsync(model.TypeGoalId);

            if (goaltype == null)
            {
                return NotFound();
            }

            goaltype.TypeGname = model.TypeGoalName;
            goaltype.TypeGdes = model.TypeGoalDescription;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Goal type updated successfully!";
                return RedirectToAction("Index", new { id = model.TypeGoalId });
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
            ViewData["ActiveTab"] = "GoalTypes";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            var typegoal = new TypeGoal
            {
                TypeGid = model.TypeGoalId,
                TypeGname = model.TypeGoalName ?? "",
                TypeGdes = model.TypeGoalDescription
            };

            _context.TypeGoals.Add(typegoal);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        //Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewData["ActiveTab"] = "GoalTypes";

            var typegoal = _context.TypeGoals.FirstOrDefault(tg => tg.TypeGid == id);

            if (typegoal == null)
            {
                return NotFound();
            }

            var viewModel = new DeleteViewModel
            {
                TypeGoalId = typegoal.TypeGid,
                TypeGoalName = typegoal.TypeGname,
                TypeGoalDescription = typegoal.TypeGdes,
                ConfirmDelete = false
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteViewModel model)
        {
            var typegoal = await _context.TypeGoals.FirstOrDefaultAsync(tg => tg.TypeGid == id);

            if (typegoal == null)
            {
                return NotFound();
            }

            if (model.ConfirmDelete)
            {
                _context.TypeGoals.Remove(typegoal);
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
