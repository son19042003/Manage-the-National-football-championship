using Football_Management.Models;
using Football_Management.ViewModels.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Management.Controllers
{
    public class TablesController : Controller
    {
        private FootballManagementContext _context;
        public TablesController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["ActiveTab"] = "Tables";

            var club = await _context.Standings
                .Include(c => c.Club)
                .OrderByDescending(c => c.Points)
                .ThenByDescending(c => c.GoalF - c.GoalA)
                .ThenByDescending(c => c.GoalF)
                .ThenByDescending(c => c.Club.ClubName)
                .Select(c => new TablesViewModel
                {
                    ClubName = c.Club.ClubName,
                    Logo = c.Club.Logo,
                    Played = c.Played,
                    Won = c.Won,
                    Drawn = c.Drawn,
                    Lost = c.Lost,
                    GoalsFor = c.GoalF,
                    GoalsAgainst = c.GoalA,
                    GoalsDifference = c.GoalF - c.GoalA,
                    Points = c.Points
                })
                .ToListAsync();

            for (int i = 0; i < club.Count; i++)
            {
                club[i].Index = i + 1;
            }

            return View(club);
        }
    }
}
