using Football_Management.Models;
using Football_Management.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Football_Management.Controllers
{
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

			var matches = await _context.Matches
                .OrderBy(m => m.Round)
                .ThenBy(m => m.DateStart)
                .ThenBy(m => m.TimeStart)
                .Take(10)
                .Select(m => new MatchViewModel
				{
					HomeTeam = m.HomeTeamNavigation.ClubName,
					LogoHome = m.HomeTeamNavigation.Logo,
					AwayTeam = m.AwayTeamNavigation.ClubName,
					LogoAway = m.AwayTeamNavigation.Logo,
					DateStart = m.DateStart,
					TimeStart = m.TimeStart,
					GoalsH = m.GoalsH,
					GoalsA = m.GoalsA,
					Status = m.Status,
					Round = m.Round
				}).ToListAsync();

			var latestNews = await _context.News
				.Where(n => n.Status)
				.OrderBy(n => n.DateU)
				.Take(3)
				.Select(n => new NewsViewModel
				{
					Title = n.Title,
					DateUpdate = n.DateU,
					Thumbnail = n.Image
				}).ToListAsync();

			var delayedMatch = matches.Count(m => m.Status == "Delayed");

			if ((matches.Count(m => m.Status == "Full-time") + delayedMatch) == 10)
			{
				var nextMatches = await _context.Matches
				.Where(m => m.Round > matches.Last().Round)
                .OrderBy(m => m.Round)
                .ThenBy(m => m.DateStart)
                .ThenBy(m => m.TimeStart)
                .Take(10)
                .Select(m => new MatchViewModel
                {
                    HomeTeam = m.HomeTeamNavigation.ClubName,
                    LogoHome = m.HomeTeamNavigation.Logo,
                    AwayTeam = m.AwayTeamNavigation.ClubName,
                    LogoAway = m.AwayTeamNavigation.Logo,
                    DateStart = m.DateStart,
                    TimeStart = m.TimeStart,
                    GoalsH = m.GoalsH,
                    GoalsA = m.GoalsA,
                    Status = m.Status,
					Round = m.Round
                }).ToListAsync();

				var model = new HomeViewModel
				{
					NextRoundMatches = nextMatches,
					LatestNews = latestNews
				};
				return View(model);
            }

            var viewModel = new HomeViewModel
            {
                NextRoundMatches = matches,
                LatestNews = latestNews
            };
            return View(viewModel);
        }
	}
}
