using RoomManagement.Core.Contracts;
using RoomManagement.Core.DTO;
using RoomManagement.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RoomManagement.Services.RoomMangementService.VoucherSerivce
{
    public interface IVoucherRepository
    {
        Task<Voucher> GetVoucherByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IList<Voucher>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<bool> IsVoucherSlugExistedAsync(int Id, string urlSlug, CancellationToken cancellationToken = default);

        Task<bool> DeleteVoucher(int Id, CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateVoucher(Voucher voucher, CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetVouchersByQuery<T>(VoucherQuery Query, IPagingParams pagingParams, Func<IQueryable<Voucher>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);
    }
}
