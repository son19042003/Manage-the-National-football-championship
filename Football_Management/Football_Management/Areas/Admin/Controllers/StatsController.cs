using Football_Management.Areas.Admin.ViewModels.Stats;
using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Football_Management.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatsController : Controller
    {
        private readonly FootballManagementContext _context;
        public StatsController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {
            ViewData["ActiveTab"] = "Stats";

            var player = _context.Players
                .Include(p => p.Club)
                .Where(p => p.Club.IsActive)
                .OrderByDescending(p => p.Goals)
                .ThenBy(p => p.FirstName + " " + p.LastName)
                .Take(20)
                .Select(p => new IndexViewModel
                {
                    PlayerId = p.PlayerId,
                    PlayerName = p.FirstName + " " + p.LastName,
                    ClubName = p.Club.ClubName,
                    LogoUrl = p.Club.Logo,
                    Goals = p.Goals
                }).ToList();

            for (int i = 0; i < player.Count; i++)
            {
                player[i].Index = i + 1;
            }

            return View(player);
        }


        //Detail
        [HttpGet]
        public IActionResult Detail(int id)
        {
            ViewData["ActiveTab"] = "Stats";

            var viewModel = _context.Players
                .Include(p => p.Club)
                .Where(p => p.PlayerId == id)
                .Select(player => new DetailViewModel
                {
                    PlayerId = player.PlayerId,
                    FirstName = player.FirstName,
                    LastName = player.LastName,
                    AvatarUrl = player.Avatar,
                    Birthday = player.Birthday,
                    Height = player.Height,
                    Nationality = player.Nationality,
                    DomesticPlayer = player.TypePlayer ?? false,
                    Position = player.Position,
                    Number = player.Number,
                    ClubName = player.Club.ClubName,
                    LinkFb = player.LinkFb,
                    LinkIg = player.LinkIg,
                    IsInClub = player.IsInClub,
                    Goals = player.Goals ?? 0
                })
                .FirstOrDefault();

            if (viewModel == null)
            {
                return NotFound();
            }

            var player = _context.Players
                .Include(p => p.Club)
                .OrderByDescending(p => p.Goals)
                .ThenBy(p => p.FirstName + " " + p.LastName)
                .Take(20)
                .Select(p => new IndexViewModel
                {
                    PlayerId = p.PlayerId
                }).ToList();

            for (int i = 0; i < player.Count; i++)
            {
                player[i].Index = i + 1;
                if (player[i].PlayerId == viewModel.PlayerId)
                {
                    viewModel.Index = player[i].Index;
                    break;
                }
            }

            return View(viewModel);
        }


        //Reset stats
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetStats()
        {
            var clubs = _context.Clubs
                .Where(c => c.IsActive)
                .ToList();

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

            var players = _context.Players
                .Include(p => p.Club)
                .Where(p => p.Club.IsActive)
                .ToList();

            foreach (var player in players)
            {
                player.Goals = 0;
            }

            _context.SaveChanges();
            TempData["Success"] = "Stats has been reseted successfully!";
            return RedirectToAction("Index");
        }
    }
}
