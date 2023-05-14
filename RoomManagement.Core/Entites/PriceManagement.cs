using RoomManagement.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Core.Entites
{
    public class PriceManagement : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }// tên của loại giá ngày lễ giá ngày thường 
        public string UrlSlug { get; set; }
        public int Price { get; set; } //giá tiền 
        public string Description { get; set; }// Mô tả 

        public IList<Room> Rooms { get; set; }

    }
}
