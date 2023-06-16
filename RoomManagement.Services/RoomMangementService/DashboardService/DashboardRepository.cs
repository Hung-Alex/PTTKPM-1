using Microsoft.EntityFrameworkCore;
using RoomManagement.Core.DTO;
using RoomManagement.Core.Entites;
using RoomManagement.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Services.RoomMangementService.DashboardService
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly RoomManagementDbContext _context;
        public DashboardRepository(RoomManagementDbContext context)
        {
            _context = context;
        }

        public async Task<int[]> GetRoomStatusInfo()
        {
            int[] quantityRooms = new int[2];
            int empty = _context.Set<Room>().Where(x => !x.Status).ToList().Count();
            quantityRooms[0] = empty;// phòng trống 
            quantityRooms[1] = _context.Set<Room>().Count()-empty;// phòng thuê
            return quantityRooms;
        }

        public async Task<int> GetTotalPriceTypeAsync()
        {
            return await _context.Set<PriceManagement>().CountAsync();
        }

        public async Task<int> GetTotalRoomsAsync()
        {
            return await _context.Set<Room>().CountAsync();
        }

        public async Task<int> GetTotalRoomTypesAsync()
        {
            return await _context.Set<RoomType>().CountAsync();
        }

        public async Task<int> GetTotalVoucherAsync()
        {
            return await _context.Set<Voucher>().CountAsync();
        }
    }
}
