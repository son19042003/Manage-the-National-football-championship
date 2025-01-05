using Football_Management.Models;
using Football_Management.ViewModels.Clubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Football_Management.Controllers
{
    public class ClubsController : Controller
    {
        private readonly FootballManagementContext _context;
        public ClubsController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["ActiveTab"] = "Clubs";

            var clubs = await _context.Clubs
                .Where(c => c.IsActive)
                .OrderBy(c => c.ClubName)
                .Select(c => new ClubsViewModel
                {
                    ClubId = c.ClubId,
                    ClubName = c.ClubName,
                    LogoUrl = c.Logo,
                    LinkFb = c.LinkFb,
                    LinkIg = c.LinkIg
                }).ToListAsync();

            return View(clubs);
        }


        [HttpGet]
        public async Task<IActionResult> Squad(string id)
        {
            ViewData["ActiveTab"] = "Clubs";

            var players = await _context.Players
                .Include(p => p.Club)
                .Where(p => p.ClubId == id && p.IsInClub)
                .Select(p => new SquadViewModel
                {
                    PlayerId = p.PlayerId,
                    PlayerName = p.FirstName + " " + p.LastName,
                    AvatarUrl = p.Avatar,
                    ShirtNumber = p.Number,
                    Position = p.Position,
                    Nationality = p.Nationality,
                    ClubName = p.Club.ClubName,
                    LogoUrl = p.Club.Logo,
                    Stadium = p.Club.Stadium
                }).ToListAsync();

            return View(players);
        }

        [HttpGet]
        public async Task<IActionResult> DetailPlayer(int id)
        {
            ViewData["ActiveTab"] = "Clubs";

            var player = await _context.Players
                .Include(p => p.Club)
                .Where(p => p.PlayerId == id)
                .Select(p => new DetailPlayerViewModel
                {
                    PlayerId = p.PlayerId,
                    Avatar = p.Avatar,
                    PlayerName = p.FirstName + " " + p.LastName,
                    ShirtNumber = p.Number,
                    Nationality = p.Nationality,
                    LinkFb = p.LinkFb,
                    LinkIg = p.LinkIg,
                    DateOfBirth = p.Birthday,
                    Height = p.Height,
                    ClubName = p.Club.ClubName,
                    ClubId = p.ClubId,
                    Position = p.Position,
                    Goals = p.Goals
                }).FirstOrDefaultAsync();

            if (player == null)
            {
                return NotFound();
            }

            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            player.Age = CalculateAge(player.DateOfBirth, currentDate);

            return View(player);
        }

        public int CalculateAge(DateOnly birthDate, DateOnly currentDate)
        {
            int age = currentDate.Year - birthDate.Year;

            if (currentDate < birthDate.AddYears(age))
            {
                age--;
            }

            return age;
        }

    }
}
