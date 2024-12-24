using Football_Management.Areas.Admin.ViewModels;
using Football_Management.Areas.Admin.ViewModels.Goals;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GoalsController : Controller
    {
        private readonly FootballManagementContext _context;
        public GoalsController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
        {
            ViewData["ActiveTab"] = "Goals";

            var goals = await _context.Goals
                .Include(g => g.Match)
                .Where(g => g.Match.Status == "Full-time")
                .OrderBy(g => g.Match.Round)
                .ThenBy(g => g.Match.DateStart)
                .ThenBy(g => g.Match.TimeStart)
                .ThenBy(g => g.Match.MatchId)
                .Select(g => new IndexViewModel
                {
                    GoalId = g.GoalId,
                    PlayerName = g.Player.FirstName + " " + g.Player.LastName,
                    ClubName = g.Team.ClubName,
                    TypeGoal = g.TypeG.TypeGname,
                    Round = g.Match.Round
                })
                .ToListAsync();

            int skip = (pageNumber - 1) * pageSize;
            int totalGoals = await _context.Goals.CountAsync();

            var paginatedGoals = goals
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            for (int i = 0; i < goals.Count; i++)
            {
                goals[i].Index = i + 1;
            }

            var paginatedResult = new PaginatedViewModel<IndexViewModel>
            {
                Items = paginatedGoals,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalGoals
            };

            return View(paginatedResult);
        }


        //Detail
        [HttpGet]
        public async Task<IActionResult> Detail(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "Goals";

            var viewModel = await _context.Goals
                .Include(g => g.Team)
                .Include(g => g.TypeG)
                .Include(g => g.Player)
                .Include(g => g.Match)
                .Where(g => g.GoalId == id)
                .Select(goal => new DetailViewModel
                {
                    GoalId = goal.GoalId,
                    TeamGoal = goal.Team.ClubName,
                    GoalIndex = goal.GoalIndex,
                    HomeTeam = goal.Match.HomeTeamNavigation.ClubName,
                    GoalH = goal.Match.GoalsH,
                    AwayTeam = goal.Match.AwayTeamNavigation.ClubName,
                    GoalA = goal.Match.GoalsA,
                    Round = goal.Match.Round,
                    PlayerScore = goal.Player.FirstName + " " + goal.Player.LastName,
                    TimeScore = goal.TimeScored,
                    GoalType = goal.TypeG.TypeGname
                }).FirstOrDefaultAsync();

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(int id, int pageNumber = 1)
        {
            ViewData["ActiveTab"] = "Goals";

            var goal = _context.Goals
                .Include(g => g.Team)
                .Include(g => g.TypeG)
                .Include(g => g.Player)
                .Include(g => g.Match)
                .ThenInclude(m => m.AwayTeamNavigation)
                .Include(g => g.Match)
                .ThenInclude(m => m.HomeTeamNavigation)
                .FirstOrDefault(g => g.GoalId == id);

            if (goal == null)
            {
                return NotFound();
            }

            var match = _context.Matches
                .Where(m => m.MatchId == goal.MatchId).FirstOrDefault();

            var listPlayersH = _context.Players
                .Where(g => g.ClubId == match.HomeTeam)
                .Select(lp => new SelectListItem
                {
                    Value = lp.PlayerId.ToString(),
                    Text = lp.FirstName + " " + lp.LastName + " " + $"({lp.Position})"
                })
                .ToList();

            var listPlayersA = _context.Players
                .Where(g => g.ClubId == match.AwayTeam)
                .Select(lp => new SelectListItem
                {
                    Value = lp.PlayerId.ToString(),
                    Text = lp.FirstName + " " + lp.LastName + " " + $"({lp.Position})"
                })
                .ToList();

            var listTypeGoals = _context.TypeGoals
                .Select(tg => new SelectListItem
                {
                    Value = tg.TypeGid.ToString(),
                    Text = tg.TypeGname
                })
                .ToList();

            var viewModel = new EditViewModel
            {
                GoalId = goal.GoalId,
                TeamGoal = goal.Team.ClubName,
                GoalIndex = goal.GoalIndex,
                Round = goal.Match.Round,
                HomeTeam = goal.Match.HomeTeamNavigation.ClubName,
                GoalH = goal.Match.GoalsH,
                AwayTeam = goal.Match.AwayTeamNavigation.ClubName,
                GoalA = goal.Match.GoalsA,
                PlayerId = goal.PlayerId,
                ListPlayersH = listPlayersH,
                ListPlayersA = listPlayersA,
                TypeGoalId = goal.TypeGid,
                ListTypeGoals = listTypeGoals,
                TimeScore = goal.TimeScored
            };

            if (viewModel.TeamGoal == match?.HomeTeamNavigation.ClubName)
                viewModel.IsHomeGoal = true;
            else viewModel.IsHomeGoal = false;

            ViewData["HomeTeamPlayers"] = listPlayersH;
            ViewData["AwayTeamPlayers"] = listPlayersA;

            ViewData["PageNumber"] = pageNumber;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ListPlayersH = await _context.Players
                    .Include(p => p.Club)
                    .Select(lp => new SelectListItem
                    {
                        Value = lp.PlayerId.ToString(),
                        Text = lp.FirstName + " " + lp.LastName + " " + $"({lp.Position})"
                    })
                    .ToListAsync();

                model.ListPlayersA = await _context.Players
                    .Include(p => p.Club)
                    .Select(lp => new SelectListItem
                    {
                        Value = lp.PlayerId.ToString(),
                        Text = lp.FirstName + " " + lp.LastName + " " + $"({lp.Position})"
                    })
                    .ToListAsync();

                model.ListTypeGoals = await _context.TypeGoals
                    .Select(tg => new SelectListItem
                    {
                        Value = tg.TypeGid.ToString(),
                        Text = tg.TypeGname
                    })
                    .ToListAsync();

                return View(model);
            }

            var goal = await _context.Goals
                .Include(g => g.Team)
                .Include(g => g.TypeG)
                .Include(g => g.Player)
                .Include(g => g.Match)
                .FirstOrDefaultAsync(g => g.GoalId == model.GoalId);

            if (goal == null)
            {
                return NotFound();
            }

            // Lấy giá trị từ form
            var playerIdHome = Request.Form["PlayerIdHome"];
            var playerIdAway = Request.Form["PlayerIdAway"];

            if (!string.IsNullOrEmpty(playerIdHome))
            {
                model.PlayerId = int.TryParse(playerIdHome, out int parsedId) ? parsedId : 1;
            }
            else if (!string.IsNullOrEmpty(playerIdAway))
            {
                model.PlayerId = int.TryParse(playerIdAway, out int parsedId) ? parsedId : 1;
            }

            goal.PlayerId = model.PlayerId;
            goal.TypeGid = model.TypeGoalId;
            goal.TimeScored = model.TimeScore ?? "";

            var typeGoalName = _context.TypeGoals
                .Where(t => t.TypeGid == goal.TypeGid).FirstOrDefault();

            var player = await _context.Players.FirstOrDefaultAsync(p => p.PlayerId == model.PlayerId);
            if (player != null && typeGoalName != null && typeGoalName.TypeGname != "Own Goal")
            {
                player.Goals += 1;
            }

            //Console.WriteLine($"Player goals before save: {player?.Goals}");

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMassage"] = "Goal details updated successfully!";

                int pageNumber = model.PageNumber;
                return RedirectToAction(nameof(Index), new { pageNumber });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes. Please try again.");
                return View(model);
            }
        }
    }
}
