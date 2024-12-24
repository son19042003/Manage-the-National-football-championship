using Football_Management.Areas.Admin.ViewModels.Roles;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly FootballManagementContext _context;
        public RolesController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ActiveTab"] = "Roles";

            var roles = _context.Roles
                .Select(r => new IndexViewModel
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName,
                    RoleDescription = r.RoleDes
                }).ToList();

            for (int i = 0; i < roles.Count; i++)
            {
                roles[i].Index = i + 1;
            }

            return View(roles);
        }


        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["ActiveTab"] = "Roles";

            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleId == id);

            if (role == null)
            {
                return NotFound();
            }

            var viewModel = new EditViewModel
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                RoleDescription = role.RoleDes
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var role = await _context.Roles
                .FindAsync(model.RoleId);

            if (role == null)
            {
                return NotFound();
            }

            role.RoleName = model.RoleName ?? "";
            role.RoleDes = model.RoleDescription;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Role updated successfully!";
                return RedirectToAction("Index", new { id = model.RoleId });
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
            ViewData["ActiveTab"] = "Roles";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            var role = new Role
            {
                RoleId = model.RoleId,
                RoleName = model.RoleName ?? "",
                RoleDes = model.RoleDescription
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        //Delete
        [HttpGet]
        public IActionResult Delete (int id)
        {
            ViewData["ActiveTab"] = "Roles";

            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);

            if (role == null)
            {
                return NotFound();
            }

            var viewModel = new DeleteViewModel
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                RoleDescription = role.RoleDes,
                ConfirmDelete = false
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (int id, DeleteViewModel model)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == id);

            if (role == null)
            {
                return NotFound();
            }

            if (model.ConfirmDelete)
            {
                _context.Roles.Remove(role);
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
