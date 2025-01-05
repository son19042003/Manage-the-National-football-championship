using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;

namespace Football_Management.Areas.Admin.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderViewComponent(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var username = user?.Identity?.Name ?? "Guest";
            var email = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "N/A";
            var avatar = user?.Claims.FirstOrDefault(c => c.Type == "Avatar")?.Value ?? "default.png";

            ViewData["Username"] = username;
            ViewData["Avatar"] = avatar;
            ViewData["Email"] = email;

            return View();
        }
    }
}
