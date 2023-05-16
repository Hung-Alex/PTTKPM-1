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

namespace RoomManagement.Services.RoomMangementService.RoomTypeSerivce
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly RoomManagementDbContext _context;
        public RoomTypeRepository(RoomManagementDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOrUpdateRoomType(RoomType roomType, CancellationToken cancellationToken = default)
        {
            if (roomType.Id > 0)
            {

                _context.Update(roomType);
            }
            else
            {
                _context.Add(roomType);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRoomType(int Id, CancellationToken cancellationToken = default)
        {
            var isExisted = await _context.Set<RoomType>().FindAsync(Id);
            if (isExisted == null)
            {
                return false;
            }
            _context.Remove(isExisted);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IList<RoomType>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<RoomType>().ToListAsync();
        }

        public async Task<RoomType> GetRoomTypeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<RoomType>().FindAsync(id, cancellationToken);
        }

        public async Task<IPagedList<T>> GetRoomTypesByQuery<T>(RoomTypeQuery Query, IPagingParams pagingParams, Func<IQueryable<RoomType>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<T> result = mapper(FilterRoomType(Query));

            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<bool> IsRoomTypeSlugExistedAsync(int Id, string urlSlug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<RoomType>().AnyAsync(x => x.Id != Id && x.UrlSlug == urlSlug);
        }



        private IQueryable<RoomType> FilterRoomType(RoomTypeQuery roomTypeQuery)
        {
            IQueryable<RoomType> roomTypes = _context
                .Set<RoomType>();

            if (!String.IsNullOrWhiteSpace(roomTypeQuery.UrlSlug))
            {
                roomTypes = roomTypes.Where(x => x.UrlSlug == roomTypeQuery.UrlSlug);
            }

            if (!String.IsNullOrWhiteSpace(roomTypeQuery.Name))
            {
                roomTypes = roomTypes.Where(x => x.Name.Contains(roomTypeQuery.Name));
            }

            return roomTypes;
        }
    }
}
