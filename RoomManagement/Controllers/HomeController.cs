using Microsoft.AspNetCore.Mvc;

namespace RoomManagement.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
