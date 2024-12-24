using Football_Management.Areas.Admin.ViewModels.Standing;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using static System.Reflection.Metadata.BlobBuilder;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StandingController : Controller
    {
        private readonly FootballManagementContext _context;
        public StandingController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ActiveTab"] = "Standing";

            var standing = _context.Standings
                .Include(s => s.Club)
                .Where(s => s.Club.IsActive)
                .OrderByDescending(s => s.Points)
                .ThenByDescending(s => s.GoalF - s.GoalA)
                .ThenByDescending(s => s.GoalF)
                .ThenBy(s => s.Club.ClubName)
                .Select(s => new IndexViewModel
                {
                    StandingId = s.StandingId,
                    ClubName = s.Club.ClubName,
                    LogoUrl = s.Club.Logo,
                    Played = s.Played,
                    GoalDifference = s.GoalF - s.GoalA,
                    Points = s.Points
                }).ToList();

            for (int i = 0; i < standing.Count; i++)
            {
                standing[i].Index = i + 1;
            }

            return View(standing);
        }


        //Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            var clubs = _context.Clubs
                .OrderByDescending(c => c.ClubName)
                .Where(c => c.IsActive)
                .ToList();
            if (clubs.Count != 20)
            {
                TempData["Error"] = "Not enough 20 teams to create rankings!";
                return RedirectToAction("Index");
            }

            var matches = _context.Matches.Where(m => m.Status == "Full-time").ToList();
            foreach (var club in clubs)
            {
                int matchesPlayed = matches.Count
                    (m => m.HomeTeam == club.ClubId || m.AwayTeam == club.ClubId);

                if (matchesPlayed != 38)
                {
                    TempData["Error"] = $"Team {club.ClubName} has not played all 38 matches!";
                    return RedirectToAction("Index");
                }
            }

            var existingStanding = _context.Standings.ToList();
            _context.Standings.RemoveRange(existingStanding);
            _context.SaveChanges();

            var standing = new List<Standing>();

            foreach (var club in clubs)
            {
                var entry = new Standing
                {
                    ClubId = club.ClubId,
                    Played = 0,
                    Won = 0,
                    Drawn = 0,
                    Lost = 0,
                    GoalF = 0,
                    GoalA = 0,
                    Points = 0
                };

                standing.Add(entry);
            }

            _context.SaveChanges();
            TempData["Success"] = "Rankings has been created successfully!";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Detail(int id)
        {
            ViewData["ActiveTab"] = "Standing";

            var standing = _context.Standings
                .Include(s => s.Club)
                .Where(s => s.Club.IsActive)
                .OrderByDescending(s => s.Points)
                .ThenByDescending(s => s.GoalF - s.GoalA)
                .ThenByDescending(s => s.GoalF)
                .ThenBy(s => s.Club.ClubName)
                .Select(s => new IndexViewModel
                {
                    StandingId = s.StandingId,
                    ClubName = s.Club.ClubName
                }).ToList();

            var viewModel = _context.Standings
                .Include(c => c.Club)
                .Where(c => c.StandingId == id)
                .Select(c => new DetailViewModel
                {
                    StandingId = c.StandingId,
                    ClubName = c.Club.ClubName,
                    LogoUrl = c.Club.Logo,
                    Played = c.Played,
                    Won = c.Won,
                    Drawn = c.Drawn,
                    Lost = c.Lost,
                    GoalF = c.GoalF,
                    GoalA = c.GoalA,
                    GoalDifference = c.GoalF - c.GoalA,
                    Points = c.Points,
                }).FirstOrDefault();

            if (viewModel == null)
            {
                return NotFound();
            }

            for (int i = 0; i < standing.Count; i++)
            {
                standing[i].Index = i + 1;
                if (standing[i].ClubName == viewModel.ClubName)
                {
                    viewModel.Index = standing[i].Index;
                    break;
                }
            }

            return View(viewModel);
        }


        //Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["ActiveTab"] = "Standing";

            var standing = _context.Standings
                .Include(s => s.Club)
                .FirstOrDefault(s => s.StandingId == id);

            if (standing == null)
            {
                return NotFound();
            }

            var viewModel = new EditViewModel
            {
                StandingId = standing.StandingId,
                ClubName = standing.Club.ClubName,
                LogoUrl = standing.Club.Logo,
                Played = standing.Played,
                Won = standing.Won,
                Drawn = standing.Drawn,
                Lost = standing.Lost,
                GoalF = standing.GoalF,
                GoalA = standing.GoalA,
                GoalDifference = standing.GoalF - standing.GoalA,
                Points = standing.Points
            };

            var st = _context.Standings
                .Include(s => s.Club)
                .Where(s => s.Club.IsActive)
                .OrderByDescending(s => s.Points)
                .ThenByDescending(s => s.GoalF - s.GoalA)
                .ThenByDescending(s => s.GoalF)
                .ThenBy(s => s.Club.ClubName)
                .Select(s => new IndexViewModel
                {
                    StandingId = s.StandingId,
                    ClubName = s.Club.ClubName
                }).ToList();

            if (viewModel == null)
            {
                return NotFound();
            }

            for (int i = 0; i < st.Count; i++)
            {
                st[i].Index = i + 1;
                if (st[i].ClubName == viewModel.ClubName)
                {
                    viewModel.Index = st[i].Index;
                    break;
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var standing = await _context.Standings
                .Include(s => s.Club)
                .FirstOrDefaultAsync(s => s.StandingId == model.StandingId);

            if (standing == null)
            {
                return NotFound();
            }

            standing.Points = model.Points;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMassage"] = "Stats of club updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while saving changes. Please try again.");
                return View(model);
            }
        }
    }
}
