using Microsoft.AspNetCore.Mvc;

namespace Football_Management.ViewComponents
{
    public class UserMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var username = Request.Cookies["UserLogin"] ?? HttpContext.Session.GetString("UserLogin");
            return View((object)username);
        }
    }
}
