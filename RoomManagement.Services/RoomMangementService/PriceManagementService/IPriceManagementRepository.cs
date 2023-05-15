using RoomManagement.Core.Contracts;
using RoomManagement.Core.DTO;
using RoomManagement.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Services.RoomMangementService.PriceManagementSerivce
{
    public interface IPriceManagementRepository
    {
        Task<PriceManagement> GetPriceManagementByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> IsPriceManagementSlugExistedAsync(int Id, string urlSlug, CancellationToken cancellationToken = default);

        Task<bool> DeletePriceManagement(int Id, CancellationToken cancellationToken = default);
        Task<IList<PriceManagement>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdatePriceManagement(PriceManagement priceManagement, CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetPriceManagementsByQuery<T>(PriceManagementQuery Query, IPagingParams pagingParams, Func<IQueryable<PriceManagement>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
    }
}
