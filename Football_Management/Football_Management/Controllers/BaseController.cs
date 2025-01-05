using Football_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Football_Management.Controllers
{
    public class BaseController : Controller
    {
        private readonly FootballManagementContext _context;
        public BaseController(FootballManagementContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            AutoLogin();
            base.OnActionExecuting(context);
        }

        private void AutoLogin()
        {
            if (Request.Cookies.TryGetValue("UserLogin", out string? username))
            {
                var user = _context.Accounts.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    HttpContext.Session.SetString("Username", username);
                }
            }
        }
    }
}
