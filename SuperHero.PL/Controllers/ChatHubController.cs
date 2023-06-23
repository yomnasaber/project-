using Microsoft.AspNetCore.Mvc;

namespace SuperHero.PL.Controllers
{
    public class ChatHubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
