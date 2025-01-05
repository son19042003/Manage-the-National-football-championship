using Football_Management.Models;
using Football_Management.ViewModels.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Management.Controllers
{
    public class FixturesController : Controller
    {
        private readonly FootballManagementContext _context;
        public FixturesController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["ActiveTab"] = "Fixtures";

            var matches = await _context.Matches
                .OrderBy(m => m.Round)
                .ThenBy(m => m.DateStart)
                .ThenBy(m => m.TimeStart)
                .Where(m => m.Status != "Full-time")
                .Take(10)
                .Select(m => new FixturesViewModel
                {
                    MatchId = m.MatchId,
                    HomeTeam = m.HomeTeamNavigation.ClubName,
                    LogoHome = m.HomeTeamNavigation.Logo,
                    AwayTeam = m.AwayTeamNavigation.ClubName,
                    LogoAway = m.AwayTeamNavigation.Logo,
                    DateStart = m.DateStart,
                    TimeStart = m.TimeStart,
                    Stadium = m.HomeTeamNavigation.Stadium
                }).ToListAsync();

            return View(matches);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMatches(int skip, int take)
        {
            var matches = await _context.Matches
                .OrderBy(m => m.Round)
                .ThenBy(m => m.DateStart)
                .ThenBy(m => m.TimeStart)
                .Where(m => m.Status != "Full-time")
                .Skip(skip)
                .Take(take)
                .Select(m => new FixturesViewModel
                {
                    MatchId = m.MatchId,
                    HomeTeam = m.HomeTeamNavigation.ClubName,
                    LogoHome = m.HomeTeamNavigation.Logo,
                    AwayTeam = m.AwayTeamNavigation.ClubName,
                    LogoAway = m.AwayTeamNavigation.Logo,
                    DateStart = m.DateStart,
                    TimeStart = m.TimeStart,
                    Stadium = m.HomeTeamNavigation.Stadium
                }).ToListAsync();

            return Json(matches);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            ViewData["ActiveTab"] = "Fixtures";

            var match = await _context.Matches
                .Include(m => m.HomeTeamNavigation)
                .Include(m => m.AwayTeamNavigation)
                .Where(m => m.MatchId == id)
                .FirstOrDefaultAsync();

            if (match == null)
            {
                return NotFound();
            }

            var viewModel = new DetailMatchViewModel
            {
                MatchId = match.MatchId,
                HomeTeamId = match.HomeTeam,
                HomeTeam = match.HomeTeamNavigation.ClubName,
                LogoHUrl = match.HomeTeamNavigation.Logo,
                AwayTeamId = match.AwayTeam,
                AwayTeam = match.AwayTeamNavigation.ClubName,
                LogoAUrl = match.AwayTeamNavigation.Logo,
                DateStart = match.DateStart,
                TimeStart = match.TimeStart,
                Stadium = match.HomeTeamNavigation.Stadium
            };

            return View(viewModel);
        }
    }
}
