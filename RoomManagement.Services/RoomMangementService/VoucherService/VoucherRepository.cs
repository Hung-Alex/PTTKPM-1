using RoomManagement.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Services.RoomMangementService.VoucherSerivce
{
    public class VoucherRepository : IVoucherRepository
    {
        public Task<Voucher> GetVoucherById(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
