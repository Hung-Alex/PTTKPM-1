using RoomManagement.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Core.Entites
{
    public class Voucher : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } //ten voucher
        public string UrlSlug { get; set; }
        public int Discount { get; set; } // phan tram giam gia

        public string Description { get; set; }// Mô tả 

        public IList<Room> Rooms { get; set; }
    }
}
