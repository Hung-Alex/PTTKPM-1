using Microsoft.AspNetCore.Mvc;

namespace RoomManagement.Areas.Admin.Controllers
{
    public class VoucherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
