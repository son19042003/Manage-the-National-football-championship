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

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var match = await _context.Matches
                .Include(m => m.AwayTeamNavigation)
                .Include(m => m.HomeTeamNavigation)
                .Where(m => m.MatchId == id)
                .FirstOrDefaultAsync();

            if (match == null)
            {
                return NotFound();
            }

            var score = new ScoreViewModel
            {
                MatchId = match.MatchId,
                HomeTeamId = match.HomeTeam,
                HomeTeam = match.HomeTeamNavigation.ClubName,
                GoalsH = match.GoalsH,
                LogoHUrl = match.HomeTeamNavigation.Logo,
                AwayTeamId = match.AwayTeam,
                AwayTeam = match.AwayTeamNavigation.ClubName,
                GoalsA = match.GoalsA,
                LogoAUrl = match.AwayTeamNavigation.Logo,
                DateStart = match.DateStart,
                TimeStart = match.TimeStart,
                Stadium = match.HomeTeamNavigation.Stadium,
                GoalsHHalf = 0,
                GoalsAHalf = 0
            };

            var playerScore = await _context.Goals
                .Include(p => p.Player)
                .Include(p => p.TypeG)
                .Where(p => p.MatchId == id)
                .Select(p => new PlayerScoreViewModel
                {
                    PlayerId = p.PlayerId,
                    PlayerName = p.Player.FirstName + " " + p.Player.LastName,
                    TimeScore = p.TimeScored,
                    TypeGoal = p.TypeG.TypeGname,
                    ClubId = p.Player.ClubId
                }).ToListAsync();

            for (int i = 0; i< playerScore.Count; i++)
            {
                if (CheckGoals(playerScore[i].TimeScore ?? ""))
                {
                    if (playerScore[i].ClubId == match.HomeTeam)
                    {
                        if (playerScore[i].TypeGoal != "Own Goal") score.GoalsHHalf++;
                        else score.GoalsAHalf++;
                    }
                    else
                    {
                        if (playerScore[i].TypeGoal == "Own Goal") score.GoalsHHalf++;
                        else score.GoalsAHalf++;
                    }
                }
            }

            var viewModel = new ResultDetailViewModel
            {
                Score = score,
                PlayerScore = playerScore
            };

            return View(viewModel);
        }

        public static bool CheckGoals(string timeScored)
        {
            if (string.IsNullOrWhiteSpace(timeScored))
                throw new ArgumentException("TimeScore cannot be null or empty");

            timeScored = timeScored.Replace("'", "").Trim();

            int mainTime = 0;

            if (timeScored.Contains("+"))
            {
                var part = timeScored.Split('+');
                mainTime = int.Parse(part[0]);

                if (mainTime <= 45) return true;
                else return false;
            }
            else
            {
                mainTime = int.Parse(timeScored);
                if (mainTime <= 45) return true;
                else return false;
            }
        }
    }
}
