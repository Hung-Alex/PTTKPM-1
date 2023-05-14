using RoomManagement.Core.DTO;
using RoomManagement.Core.Entites;

namespace RoomManagement.Models
{
    public class RoomItem
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

        public RoomTypeDto RoomType { get; set; }
      
        public VoucherDto Voucher { get; set; }
       
        public PriceManagementDto PriceManagement { get; set; }
       
    }
}
