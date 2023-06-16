using Microsoft.AspNetCore.Mvc;
using RoomManagement.Services.RoomMangementService.DashboardService;

namespace RoomManagement.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["TotalRoomTypes"] = await _dashboardRepository.GetTotalRoomTypesAsync();
            ViewData["TotalRooms"] = await _dashboardRepository.GetTotalRoomsAsync();
            ViewData["TotalVoucher"] = await _dashboardRepository.GetTotalVoucherAsync();
            ViewData["TotalPriceType"] = await _dashboardRepository.GetTotalPriceTypeAsync();

            ViewData["Analysis"] = await _dashboardRepository.GetRoomStatusInfo();
            return View();  
        }
    }
}
