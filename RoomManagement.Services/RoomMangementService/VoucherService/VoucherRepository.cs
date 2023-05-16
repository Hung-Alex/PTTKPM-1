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

namespace RoomManagement.Services.RoomMangementService.VoucherSerivce
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly RoomManagementDbContext _context;
        public VoucherRepository(RoomManagementDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOrUpdateVoucher(Voucher voucher, CancellationToken cancellationToken = default)
        {
            if (voucher.Id > 0)
            {

                _context.Update(voucher);
            }
            else
            {
                _context.Add(voucher);
            }
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> DeleteVoucher(int Id, CancellationToken cancellationToken = default)
        {
            var isExisted = await _context.Set<Voucher>().FindAsync(Id);
            if (isExisted == null)
            {
                return false;
            }
            _context.Remove(isExisted);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IList<Voucher>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Voucher>().ToListAsync();
        }

        public async Task<Voucher> GetVoucherByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Voucher>().FindAsync(id, cancellationToken);
        }


        public async Task<IPagedList<T>> GetVouchersByQuery<T>(VoucherQuery Query, IPagingParams pagingParams, Func<IQueryable<Voucher>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<T> result = mapper(FilterVoucher(Query));

            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }


        public async Task<bool> IsVoucherSlugExistedAsync(int Id, string urlSlug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Voucher>().AnyAsync(x => x.Id != Id && x.UrlSlug == urlSlug);
        }


        private IQueryable<Voucher> FilterVoucher(VoucherQuery voucherQuery)
        {
            IQueryable<Voucher> vouchers = _context
                .Set<Voucher>();          

            if (!String.IsNullOrWhiteSpace(voucherQuery.UrlSlug))
            {
                vouchers = vouchers.Where(x => x.UrlSlug == voucherQuery.UrlSlug);
            }

            if (!String.IsNullOrWhiteSpace(voucherQuery.Name))
            {
                vouchers = vouchers.Where(x => x.Name.Contains(voucherQuery.Name));
            }
           
            return vouchers;
        }
    }
}
