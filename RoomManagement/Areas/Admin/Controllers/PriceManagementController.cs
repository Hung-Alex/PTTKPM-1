using Microsoft.AspNetCore.Mvc;

namespace RoomManagement.Areas.Admin.Controllers
{
    public class PriceManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
