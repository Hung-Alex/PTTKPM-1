using Mapster;
using Microsoft.Extensions.Hosting;
using RoomManagement.Areas.Admin.Models.RoomModel;
using RoomManagement.Core.DTO;
using RoomManagement.Core.Entites;
using RoomManagement.Models;

namespace RoomManagement.Mapster
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Room, RoomDto>();
            config.NewConfig<Room, RoomItem>();
            config.NewConfig<RoomQuery, RoomFilterModel>();


            config.NewConfig<RoomType, RoomTypeDto>();
            config.NewConfig<RoomType, RoomTypeItem>();


            config.NewConfig<Voucher, VoucherDto>();
            config.NewConfig<Voucher, VoucherItem>();


            config.NewConfig<PriceManagement, PriceManagementDto>();
            config.NewConfig<PriceManagement, PriceManagementItem>();








        }
    }
}
