using Football_Management.Models;
using Football_Management.ViewModels.Fixtures;
using Football_Management.ViewModels.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Management.Controllers
{
    public class ResultsController : Controller
    {
        private readonly FootballManagementContext _context;
        public ResultsController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["ActiveTab"] = "Results";

            var matches = await _context.Matches
                .OrderByDescending(m => m.Round)
                .ThenByDescending(m => m.DateStart)
                .ThenByDescending(m => m.TimeStart)
                .Where(m => m.Status == "Full-time")
                .Take(10)
                .Select(m => new ResultsViewModel
                {
                    MatchId = m.MatchId,
                    HomeTeam = m.HomeTeamNavigation.ClubName,
                    LogoHome = m.HomeTeamNavigation.Logo,
                    GoalsH = m.GoalsH,
                    AwayTeam = m.AwayTeamNavigation.ClubName,
                    LogoAway = m.AwayTeamNavigation.Logo,
                    GoalsA = m.GoalsA,
                    DateStart = m.DateStart,
                    Stadium = m.HomeTeamNavigation.Stadium
                }).ToListAsync();

            return View(matches);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMatches(int skip, int take)
        {
            var matches = await _context.Matches
                .OrderByDescending(m => m.Round)
                .ThenByDescending(m => m.DateStart)
                .ThenByDescending(m => m.TimeStart)
                .Where(m => m.Status == "Full-time")
                .Skip(skip)
                .Take(take)
                .Select(m => new ResultsViewModel
                {
                    MatchId = m.MatchId,
                    HomeTeam = m.HomeTeamNavigation.ClubName,
                    LogoHome = m.HomeTeamNavigation.Logo,
                    GoalsH = m.GoalsH,
                    AwayTeam = m.AwayTeamNavigation.ClubName,
                    LogoAway = m.AwayTeamNavigation.Logo,
                    GoalsA = m.GoalsA,
                    DateStart = m.DateStart,
                    Stadium = m.HomeTeamNavigation.Stadium
                }).ToListAsync();

            return Json(matches);
        }
    }
}
