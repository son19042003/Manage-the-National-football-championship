using Football_Management.Areas.Admin.ViewModels;
using Football_Management.Areas.Admin.ViewModels.Matches;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MatchesController : Controller
    {
        private readonly FootballManagementContext _context;
        public MatchesController(FootballManagementContext context)
        {
            _context = context;
        }

        //GET
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            ViewData["ActiveTab"] = "Matches";

            var matches = await _context.Matches.Select(c => new IndexViewModel
            {
                MatchId = c.MatchId,
                Round = c.Round,
                HomeTeam = c.HomeTeamNavigation.ClubName,
                AwayTeam = c.AwayTeamNavigation.ClubName,
                Status = c.Status,
                TimeStart = c.TimeStart,
                DateStart = c.DateStart
            })
            .OrderBy(m => m.DateStart)
            .ThenBy(m => m.TimeStart)
            .ToListAsync();

            var fulltimeRounds = matches
                .GroupBy(m => m.Round)
                .Where(g => g.All(m => m.Status == "Full Time"))
                .Select(g => g.Key)
                .ToHashSet();

            var sortedMatches = matches
                .OrderBy(m => fulltimeRounds.Contains(m.Round))
                .ThenBy(m => m.Round)
                .ToList();

            int numOfM = 1;
            for (int i = 0; i < sortedMatches.Count; i++)
            {
                sortedMatches[i].Index = numOfM;
                numOfM++;
                if (numOfM == 11)
                {
                    numOfM = 1;
                }
            }

            int skip = (pageNumber - 1) * pageSize;
            int totalMatches = await _context.Matches.CountAsync();

            var paginatedMatches = sortedMatches
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            var paginatedResult = new PaginatedViewModel<IndexViewModel>
            {
                Items = paginatedMatches,
                TotalItems = totalMatches,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return View(paginatedResult);
        }

        //Create
        //POST
        [HttpPost]
        public IActionResult Create()
        {
            AutomationCreateMatches();
            return RedirectToAction("Index");
        }

        public void AutomationCreateMatches()
        {
            var teams = _context.Clubs.Where(c => c.IsActive).ToList();
            var matches = new List<Match>();

            int totalRouns = teams.Count*2 - 2;
            Random rand = new Random();
            teams.Sort((x, y) => rand.Next(-1, 2));

            for (int round = 0; round < totalRouns/2; round++)
            {
                for (int i = 0; i < teams.Count/2; i++)
                {
                    var home = teams[i].ClubId;
                    var away = teams[teams.Count - 1 - i].ClubId;
                    var match = new Match
                    {
                        Round = round + 1,
                        TimeStart = TimeOnly.MinValue,
                        DateStart = DateOnly.MinValue,
                        HomeTeam = home,
                        AwayTeam = away,
                        Status = "Scheduled"
                    };

                    matches.Add(match);

                    var returnMatch = new Match
                    {
                        Round = totalRouns - round,
                        TimeStart = TimeOnly.MinValue,
                        DateStart = DateOnly.MinValue,
                        HomeTeam = away,
                        AwayTeam = home,
                        Status = "Scheduled"
                    };

                    matches.Add(returnMatch);
                }
                var firstTeam = teams[0];
                teams.RemoveAt(0);
                teams.Add(firstTeam);
            }

            _context.Matches.AddRange(matches);
            _context.SaveChanges();
        }

        public string GetMatchStatus(Match match)
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime matchDateTime = match.DateStart.ToDateTime(match.TimeStart);

            if (currentDateTime.Date < match.DateStart.ToDateTime(new TimeOnly()))
            {
                return "Scheduled";
            }
            else if (currentDateTime.Date == match.DateStart.ToDateTime(new TimeOnly()) && currentDateTime.TimeOfDay < match.TimeStart.ToTimeSpan())
            {
                return "Soon";
            }

            return "Live";
        }
    }
}
