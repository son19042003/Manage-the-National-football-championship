using Football_Management.Areas.Admin.ViewModels.Home;
using Football_Management.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Football_Management.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
	{
		private readonly FootballManagementContext _context;

		public HomeController(FootballManagementContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			ViewData["ActiveTab"] = "Home";

			var registrations = await _context.PlayerRegistrations
				.Where(r => !r.IsProcessed ?? false == false)
                .OrderByDescending(r => r.PlayerRegisId)
				.Select(r => new RegistrationPlayersViewModel
				{
					RegistrationPlayerId = r.PlayerRegisId,
					ClubName = r.Club.ClubName,
					Name = r.FirstName + " " + r.LastName,
					Position = r.Position,
					Nationality = r.Nationality
				})
            .ToListAsync();

            for (int i = 0; i < registrations.Count; i++)
            {
                registrations[i].Index = i + 1;
            }

            return View(registrations);
		}


        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewData["ActiveTab"] = "Home";

            var registration = _context.PlayerRegistrations
                .Include(r => r.Club)
                .FirstOrDefault(r => r.PlayerRegisId == id);

            if (registration == null)
            {
                return NotFound();
            }

            var viewModel = new DeleteViewModel
            {
                PlayerRegisId = registration.PlayerRegisId,
                Name = registration.FirstName + " " + registration.LastName,
                DateOfBirth = registration.Birthday,
                Height = registration.Height,
                Position = registration.Position,
                Nationality = registration.Nationality,
                Number = registration.Number,
                AvatarPath = registration.Avatar,
                LinkFb = registration.LinkFb,
                LinkIg = registration.LinkIg,
                Club = registration.Club.ClubName,
                ConfirmDelete = false
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, DeleteViewModel model)
        {
            var registration = await _context.PlayerRegistrations
                .Include(r => r.Club)
                .FirstOrDefaultAsync(r => r.PlayerRegisId == id);

            if (registration == null)
            {
                return NotFound();
            }

            if (model.ConfirmDelete)
            {
                _context.PlayerRegistrations.Remove(registration);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
