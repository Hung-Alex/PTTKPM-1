using Microsoft.AspNetCore.Mvc;

namespace RoomManagement.Areas.Admin.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
