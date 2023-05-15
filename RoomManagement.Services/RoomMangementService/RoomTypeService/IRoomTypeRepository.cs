using RoomManagement.Core.Contracts;
using RoomManagement.Core.DTO;
using RoomManagement.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Services.RoomMangementService.RoomTypeSerivce
{
    public interface IRoomTypeRepository
    {
        Task<RoomType> GetRoomTypeByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IList<RoomType>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<bool> IsRoomTypeSlugExistedAsync(int Id, string urlSlug, CancellationToken cancellationToken = default);

        Task<bool> DeleteRoomType(int Id, CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateRoomType(RoomType roomType, CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetRoomTypesByQuery<T>(RoomTypeQuery Query, IPagingParams pagingParams, Func<IQueryable<RoomType>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
    }
}
