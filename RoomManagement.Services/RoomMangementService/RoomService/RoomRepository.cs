using Microsoft.EntityFrameworkCore;
using RoomManagement.Core.Contracts;
using RoomManagement.Core.DTO;
using RoomManagement.Core.Entites;
using RoomManagement.Data.Context;
using RoomManagement.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Services.RoomMangementService.RoomSerivce
{
    public class RoomRepository:IRoomRepository
    {
        private readonly RoomManagementDbContext _context;
        public RoomRepository(RoomManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrUpdateRoom(Room room, CancellationToken cancellationToken = default)
        {
            if (room.Id > 0)
            {

                _context.Update(room);
            }
            else
            {
                _context.Add(room);
            }
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteRoom(int Id, CancellationToken cancellationToken = default)
        {
            var isExisted = await _context.Set<Room>().FindAsync(Id);
            if (isExisted == null)
            {
                return false;
            }
            _context.Remove(isExisted);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }


        public async Task<Room> GetRoomByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Room>().FindAsync(id, cancellationToken);
        }


        public async Task<IPagedList<T>> GetRoomsByQuery<T>(RoomQuery Query, IPagingParams pagingParams, Func<IQueryable<Room>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<T> result = mapper(FilterRoom(Query));

            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }


        public async Task<bool> IsRoomSlugExistedAsync(int Id, string urlSlug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Room>().AnyAsync(x => x.Id != Id && x.UrlSlug == urlSlug);
        }

        private IQueryable<Room> FilterRoom(RoomQuery roomQuery)
        {
            IQueryable<Room> rooms  = _context
                .Set<Room>()
                .Include(x=>x.Voucher)
                .Include(x=>x.PriceManagement)
                .Include(x=>x.RoomType);

            if (!String.IsNullOrWhiteSpace(roomQuery.UrlSlug))
            {
                rooms = rooms.Where(x => x.UrlSlug == roomQuery.UrlSlug);
            }

            if (!String.IsNullOrWhiteSpace(roomQuery.Name))
            {
                rooms = rooms.Where(x => x.Name.Contains(roomQuery.Name));
            }
            if(roomQuery.Status!=null)
            {
                rooms = rooms.Where(x => x.Status == roomQuery.Status);
            }
            if (roomQuery.Area>0)
            {
                rooms = rooms.Where(x => x.Area*1 == roomQuery.Area*1);
            }
            if (roomQuery.Height > 0)
            {
                rooms = rooms.Where(x => x.Height * 1 == roomQuery.Height * 1);
            }
            if (roomQuery.Width > 0)
            {
                rooms = rooms.Where(x => x.Width * 1 == roomQuery.Width * 1);
            }
            //---------------------------------------------------------
            if (!String.IsNullOrWhiteSpace(roomQuery.RoomTypeSlug))
            {
                rooms = rooms.Where(x => x.RoomType.UrlSlug == roomQuery.RoomTypeSlug);
            }
            if (!String.IsNullOrWhiteSpace(roomQuery.PriceManagementSlug))
            {
                rooms = rooms.Where(x => x.PriceManagement.UrlSlug == roomQuery.PriceManagementSlug);
            }
            if (!String.IsNullOrWhiteSpace(roomQuery.VoucherSlug))
            {
                rooms = rooms.Where(x => x.Voucher.UrlSlug == roomQuery.VoucherSlug);
            }
           //---------------------------------------------------------------------
            if (roomQuery.RoomTypeId>0)
            {
                rooms = rooms.Where(x => x.RoomType.Id == roomQuery.RoomTypeId);
            }
            if (roomQuery.PriceManagementId > 0)
            {
                rooms = rooms.Where(x => x.PriceManagement.Id == roomQuery.PriceManagementId);
            }
            if (roomQuery.VoucherId > 0)
            {
                rooms = rooms.Where(x => x.Voucher.Id == roomQuery.VoucherId);
            }
            //------------------------------------------------------------------------


            return rooms;
        }
    }
}
