using Mapster;
using Microsoft.Extensions.Hosting;
using RoomManagement.Core.DTO;
using RoomManagement.Core.Entites;

namespace RoomManagement.Mapster
{
    public class MapsterConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Room, RoomQuery>();
                 

        }
    }
}
