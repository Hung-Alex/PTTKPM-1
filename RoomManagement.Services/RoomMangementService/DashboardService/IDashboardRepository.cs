using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Services.RoomMangementService.DashboardService
{
    public interface IDashboardRepository
    {
        Task<int> GetTotalRoomTypesAsync();

        Task<int> GetTotalRoomsAsync();

        Task<int> GetTotalVoucherAsync();

        Task<int> GetTotalPriceTypeAsync();

        Task<int[]> GetRoomStatusInfo();
    }
}
