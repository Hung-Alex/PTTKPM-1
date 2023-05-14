using Microsoft.AspNetCore.Mvc;

namespace RoomManagement.Areas.Admin.Controllers
{
    public class RoomTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
