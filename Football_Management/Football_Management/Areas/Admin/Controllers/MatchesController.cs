using Football_Management.Areas.Admin.ViewModels;
using Football_Management.Areas.Admin.ViewModels.Matches;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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


        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            ViewData["ActiveTab"] = "Matches";

            var matches = await _context.Matches.Select(c => new IndexViewModel
            {
                MatchId = c.MatchId,
                Round = c.Round,
                HomeTeam = c.HomeTeamNavigation.ClubName,
                LogoHomeTeam = c.HomeTeamNavigation.Logo,
                AwayTeam = c.AwayTeamNavigation.ClubName,
                LogoAwayTeam = c.AwayTeamNavigation.Logo,
                Status = c.Status,
                TimeStart = c.TimeStart,
                DateStart = c.DateStart,
                GoalsH = c.GoalsH,
                GoalsA = c.GoalsA
            })
            .OrderBy(m => m.DateStart)
            .ThenBy(m => m.TimeStart)
            .ThenBy(m => m.MatchId)
            .ToListAsync();

            var fulltimeRounds = matches
                .GroupBy(m => m.Round)
                .Where(g => g.All(m => m.Status == "Full-time"))
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
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            //check
            var clubs = _context.Clubs
                .OrderByDescending(c => c.ClubName)
                .Where(c => c.IsActive)
                .ToList();
            if (clubs.Count != 20)
            {
                TempData["Error"] = "Not enough 20 teams to create rankings!";
                return RedirectToAction("Index");
            }

            var matchesCheck = _context.Matches.Where(m => m.Status == "Full-time").ToList();
            foreach (var club in clubs)
            {
                int matchesPlayed = matchesCheck.Count
                    (m => m.HomeTeam == club.ClubId || m.AwayTeam == club.ClubId);

                if (matchesPlayed != 38)
                {
                    TempData["Error"] = $"Team {club.ClubName} has not played all 38 matches!";
                    return RedirectToAction("Index");
                }
            }

            _context.Matches.ExecuteDelete();

            //create
            var teams = _context.Clubs.Where(c => c.IsActive).ToList();
            var matches = new List<Match>();

            int totalRouns = teams.Count * 2 - 2;
            Random rand = new Random();
            teams.Sort((x, y) => rand.Next(-1, 2));

            for (int round = 0; round < totalRouns / 2; round++)
            {
                for (int i = 0; i < teams.Count / 2; i++)
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

            return RedirectToAction("Index");
        }


        //Detail
        //GET
        [HttpGet]
        public async Task<IActionResult> Detail(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "Matches";

            var viewModel = await _context.Matches
                .Include(m => m.HomeTeamNavigation)
                .Include(m => m.AwayTeamNavigation)
                .Where(c => c.MatchId == id)
                .Select(match => new DetailViewModel
                {
                    MatchId = match.MatchId,
                    Round = match.Round,
                    HomeTeam = match.HomeTeamNavigation.ClubName,
                    AwayTeam = match.AwayTeamNavigation.ClubName,
                    Status = match.Status,
                    Stadium = match.HomeTeamNavigation.Stadium,
                    TimeStart = match.TimeStart,
                    DateStart = match.DateStart,
                    GoalH = match.GoalsH ?? 0,
                    GoalA = match.GoalsA ?? 0,
                    LogoHomeTeam = match.HomeTeamNavigation.Logo,
                    LogoAwayTeam = match.AwayTeamNavigation.Logo
                })
                .FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return NotFound();
            }

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }

        //Edit
        //GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "Matches";

            var match = await _context.Matches
                .Include(m => m.HomeTeamNavigation)
                .Include(m => m.AwayTeamNavigation)
                .FirstOrDefaultAsync(m => m.MatchId == id);

            if (match == null)
            {
                return NotFound();
            }

            var viewModel = new EditViewModel
            {
                MatchId = match.MatchId,
                Round = match.Round,
                HomeTeam = match.HomeTeamNavigation.ClubName,
                AwayTeam = match.AwayTeamNavigation.ClubName,
                Status = match.Status,
                StatusOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Delayed", Text = "Delayed" },
                    new SelectListItem { Value = "Scheduled", Text = "Scheduled" },
                    new SelectListItem { Value = "Live", Text = "Live" },
                    new SelectListItem { Value = "Half-time", Text = "Half-time" },
                    new SelectListItem { Value = "Full-time", Text = "Full-time" }
                },
                Stadium = match.HomeTeamNavigation.Stadium,
                TimeStart = match.TimeStart,
                DateStart = match.DateStart,
                GoalH = match.GoalsH ?? 0,
                GoalA = match.GoalsA ?? 0
            };

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.StatusOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Delayed", Text = "Delayed" },
                    new SelectListItem { Value = "Scheduled", Text = "Scheduled" },
                    new SelectListItem { Value = "Live", Text = "Live" },
                    new SelectListItem { Value = "Half-time", Text = "Half-time" },
                    new SelectListItem { Value = "Full-time", Text = "Full-time" }
                };
                //return View(viewModel);
            }


            var match = await _context.Matches
                .Include(m => m.HomeTeamNavigation)
                .Include(m => m.AwayTeamNavigation)
                .FirstOrDefaultAsync(m => m.MatchId == viewModel.MatchId);

            if (match == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin trận đấu
            match.Status = viewModel.Status ?? "Scheduled";
            match.TimeStart = viewModel.TimeStart;
            match.DateStart = viewModel.DateStart;
            match.GoalsH = viewModel.GoalH;
            match.GoalsA = viewModel.GoalA;

            if (match.Status == "Full-time")
            {
                for (int i = 0; i < match.GoalsH; i++)
                {
                    var homeGoals = new Goal
                    {
                        MatchId = match.MatchId,
                        TeamId = match.HomeTeamNavigation.ClubId,
                        PlayerId = 1,
                        TimeScored = "0",
                        TypeGid = 1,
                        GoalIndex = i + 1
                    };

                    _context.Goals.Add(homeGoals);
                }

                for (int i = 0; i < match.GoalsA; i++)
                {
                    var awayGoals = new Goal
                    {
                        MatchId = match.MatchId,
                        TeamId = match.AwayTeamNavigation.ClubId,
                        PlayerId = 1,
                        TimeScored = "0",
                        TypeGid = 1,
                        GoalIndex = i + 1
                    };

                    _context.Goals.Add(awayGoals);
                }
            }

            if (match.Status == "Full-time")
            {
                var clubs = _context.Clubs
                    .Where(c => c.IsActive)
                    .Where(c => c.ClubId == match.HomeTeam || c.ClubId == match.AwayTeam)
                    .ToList();

                foreach (var club in clubs)
                {
                    var standing = _context.Standings
                            .Where(c => c.ClubId == club.ClubId).FirstOrDefault();

                    if (standing == null)
                    {
                        break;
                    }
                    standing.Played++;

                    if (club.ClubId == match.HomeTeam)
                    {
                        standing.GoalF += match.GoalsH;
                        standing.GoalA += match.GoalsA;
                        if (match.GoalsH > match.GoalsA)
                        {
                            standing.Won++;
                            standing.Points = standing.Points + 3;
                        }
                        else if (match.GoalsH == match.GoalsA)
                        {
                            standing.Drawn++;
                            standing.Points = standing.Points + 1;
                        }
                        else
                        {
                            standing.Lost++;
                            standing.Points = standing.Points + 0;
                        }
                    }
                    else
                    {
                        standing.GoalF += match.GoalsA;
                        standing.GoalA += match.GoalsH;
                        if (match.GoalsH < match.GoalsA)
                        {
                            standing.Won++;
                            standing.Points += 3;
                        }
                        else if (match.GoalsH == match.GoalsA)
                        {
                            standing.Drawn++;
                            standing.Points += 1;
                        }
                        else
                        {
                            standing.Lost++;
                            standing.Points += 0;
                        }
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Match updated successfully!";

                int pageNumber = viewModel.PageNumber;
                return RedirectToAction(nameof(Index), new { pageNumber });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred while saving changes: {ex.Message}");
            }

            viewModel.StatusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "Delayed", Text = "Delayed" },
                new SelectListItem { Value = "Scheduled", Text = "Scheduled" },
                new SelectListItem { Value = "Live", Text = "Live" },
                new SelectListItem { Value = "Half-time", Text = "Half-time" },
                new SelectListItem { Value = "Full-time", Text = "Full-time" }
            };

            return View(viewModel);
        }
    }
}
