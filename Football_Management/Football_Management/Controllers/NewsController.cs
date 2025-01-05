using Football_Management.Models;
using Football_Management.ViewModels.News;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Text.Json.Nodes;

namespace Football_Management.Controllers
{
    public class NewsController : Controller
    {
        private readonly FootballManagementContext _context;
        public NewsController(FootballManagementContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> InjuryNews(int page = 1, int pageSize = 12)
        {
            ViewData["ActiveTab"] = "News";

            var (injuryNews, totalPages) = await GetPaginatedNews("Injury", page, pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(injuryNews);
        }


        [HttpGet]
        public async Task<IActionResult> TransferNews(int page = 1, int pageSize = 12)
        {
            ViewData["ActiveTab"] = "News";

            var (transferNews, totalPages) = await GetPaginatedNews("Transfer", page, pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(transferNews);
        }


        [HttpGet]
        public async Task<IActionResult> NormalNews(int page = 1, int pageSize = 12)
        {
            ViewData["ActiveTab"] = "News";

            var (normalNews, totalPages) = await GetPaginatedNews("Normal", page, pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(normalNews);
        }

        public async Task<(List<NewsViewModel>, int)> GetPaginatedNews(string newsType, int page, int pageSize)
        {
            var totalNews = await _context.News
                .Where(n => n.TypeNews.TypeNewsName == newsType && n.Status)
                .CountAsync();

            int totalPages = (int)Math.Ceiling(totalNews / (double)pageSize);

            var news = await _context.News
                .Where(n => n.TypeNews.TypeNewsName == newsType && n.Status)
                .OrderBy(n => n.DateU)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(n => new NewsViewModel
                {
                    NewsId = n.NewsId,
                    Title = n.Title,
                    ThumnailUrl = n.Image,
                    DateUpdate = n.DateU
                }).ToListAsync();

            return (news, totalPages);
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            ViewData["ActiveTab"] = "News";

            var news = await _context.News
                .Where(n => n.NewsId == id)
                .FirstOrDefaultAsync();

            if (news == null)
            {
                return NotFound();
            }

            var newsDetail = new DetailNewsViewModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                ThumnailUrl= news.Image,
                DateUpdate = news.DateU,
                Content = news.Content,
                ImageContent = news.ImgContent
            };

            var latestNews = await _context.News
                .Where(n => n.Status && n.NewsId != id)
                .OrderBy(n => n.DateU)
                .Take(4)
                .Select(n => new LatestNewsViewModel
                {
                    NewsId = n.NewsId,
                    Title = n.Title,
                    ThumnailUrl = n.Image,
                    DateUpdate = n.DateU
                }).ToListAsync();

            var viewModel = new DetailViewModel
            {
                DetailNews = newsDetail,
                LatestNews = latestNews
            };

            return View(viewModel);
        }
    }
}
