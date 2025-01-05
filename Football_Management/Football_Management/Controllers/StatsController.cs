using Football_Management.Models;
using Football_Management.ViewModels.Stats;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Management.Controllers
{
    public class StatsController : Controller
    {
        private readonly FootballManagementContext _context;
        public StatsController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            ViewData["ActiveTab"] = "Stats";

            var player = await _context.Players
                .Include(p => p.Club)
                .Where(p => p.Club.IsActive && p.Goals > 0)
                .OrderByDescending(p => p.Goals)
                .ThenBy(p => p.FirstName + " " + p.LastName)
                .Select(p => new StatsViewModel
                {
                    PlayerId = p.PlayerId,
                    PlayerName = p.FirstName + " " + p.LastName,
                    ClubName = p.Club.ClubName,
                    LogoUrl = p.Club.Logo,
                    Goals = p.Goals,
                    ClubId = p.ClubId,
                    Rank = 1
                }).ToListAsync();

            int skip = 1;

            for (int i = 1; i < player.Count; i++)
            {
                if (player[i].Goals == player[i - 1].Goals)
                {
                    player[i].Rank = player[i - 1].Rank;
                    skip++;
                }
                else
                {
                    player[i].Rank = player[i - 1].Rank + skip;
                    skip = 1;
                }
            }

            int totalPlayers = player.Count();
            var playersOnePage = player
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalPlayers / pageSize);
            ViewBag.CurrentPage = page;

            return View(playersOnePage);
        }
    }
}
