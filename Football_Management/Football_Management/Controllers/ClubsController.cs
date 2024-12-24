using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Football_Management.Controllers
{
    public class ClubsController : Controller
    {
        private readonly FootballManagementContext _context;
        public ClubsController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["ActiveTab"] = "Clubs";

            return View();
        }
    }
}
