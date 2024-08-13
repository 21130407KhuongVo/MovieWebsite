using Microsoft.AspNetCore.Mvc;

namespace MovieWebsite.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Admin/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
