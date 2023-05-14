using Microsoft.AspNetCore.Mvc;

namespace RoomManagement.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
