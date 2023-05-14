using RoomManagement.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Core.Entites
{
    public class Room : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }// tên loại phòng
        public string UrlSlug { get; set; }
        public string Image { get; set; }// hinh anh
        public string Video { get; set; } // video
        public bool Status { get; set; } // trạng thái phòng - trống or thuê
        public float Area { get; set; }// Diện tích đơn vị là mét ( m )
        public string Description { get; set; }//Mo ta
        public float Height { get; set; } //cao đơn vị là mét ( m )
        public float Width { get; set; } //rộng đơn vị là mét ( m )

        public RoomType RoomType { get; set; }
        public int RoomTypeId { get; set; } //mã loại phòng
        public Voucher Voucher { get; set; }
        public int VoucherId { get; set;}// mã voucher
        public PriceManagement PriceManagement { get; set; }
        public int PriceManagementId { get; set; } // mã quản lý giá 


    }
}
