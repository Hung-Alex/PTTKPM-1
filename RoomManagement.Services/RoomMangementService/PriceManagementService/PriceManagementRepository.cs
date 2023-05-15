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

namespace RoomManagement.Services.RoomMangementService.PriceManagementSerivce
{
    public class PriceManagementRepository: IPriceManagementRepository
    {
        private readonly RoomManagementDbContext _context;
        public PriceManagementRepository(RoomManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddOrUpdatePriceManagement(PriceManagement priceManagement, CancellationToken cancellationToken = default)
        {
            if (priceManagement.Id > 0)
            {

                _context.Update(priceManagement);
            }
            else
            {
                _context.Add(priceManagement);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePriceManagement(int Id, CancellationToken cancellationToken = default)
        {
            var isExisted = await _context.Set<PriceManagement>().FindAsync(Id);
            if (isExisted == null)
            {
                return false;
            }
            _context.Remove(isExisted);
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IPagedList<T>> GetPriceManagementsByQuery<T>(PriceManagementQuery Query, IPagingParams pagingParams, Func<IQueryable<PriceManagement>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<T> result = mapper(FilterPriceManagement(Query));

            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<PriceManagement> GetPriceManagementByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<PriceManagement>().FindAsync(id, cancellationToken);
        }

        public async Task<bool> IsPriceManagementSlugExistedAsync(int Id, string urlSlug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Voucher>().AnyAsync(x => x.Id != Id && x.UrlSlug == urlSlug);
        }

        private IQueryable<PriceManagement> FilterPriceManagement(PriceManagementQuery priceManagementQuery)
        {
            IQueryable<PriceManagement> priceManagements = _context
                .Set<PriceManagement>();

            if (!String.IsNullOrWhiteSpace(priceManagementQuery.UrlSlug))
            {
                priceManagements = priceManagements.Where(x => x.UrlSlug == priceManagementQuery.UrlSlug);
            }

            if (!String.IsNullOrWhiteSpace(priceManagementQuery.Name))
            {
                priceManagements = priceManagements.Where(x => x.Name.Contains(priceManagementQuery.Name));
            }

            return priceManagements;
        }

        public async Task<IList<PriceManagement>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<PriceManagement>().ToListAsync();
        }
    }
}
