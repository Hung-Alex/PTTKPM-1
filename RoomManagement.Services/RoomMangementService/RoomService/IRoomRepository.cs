using RoomManagement.Core.Contracts;
using RoomManagement.Core.DTO;
using RoomManagement.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Services.RoomMangementService.RoomSerivce
{
    public interface IRoomRepository
    {
        Task<Room> GetRoomByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> IsRoomSlugExistedAsync(int Id, string urlSlug, CancellationToken cancellationToken = default);

        Task<bool> DeleteRoom(int Id, CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateRoom(Room roomType, CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetRoomsByQuery<T>(RoomQuery Query, IPagingParams pagingParams, Func<IQueryable<Room>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
    }
}
