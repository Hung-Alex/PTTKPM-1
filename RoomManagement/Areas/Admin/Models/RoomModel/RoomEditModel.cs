using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoomManagement.Core.Entites;
using System.ComponentModel;

namespace RoomManagement.Areas.Admin.Models.RoomModel
{
    public class RoomEditModel
    {
        public int Id { get; set; }

        [DisplayName("Tên phòng")]
        public string Name { get; set; }// tên loại phòng

        [DisplayName("Slug")]
        [Remote("VerifyRoomSlug", "Room", "Admin", HttpMethod = "POST", AdditionalFields = "Id")]
        public string UrlSlug { get; set; }

        [DisplayName("Hình hiện tại")]
        public string Image { get; set; }// hinh anh

        [DisplayName("Chọn hình ảnh")]
        public IFormFile ImageFile { get; set; }

        [DisplayName("Video")]
        public string Video { get; set; } // video

        [DisplayName("Trạng thái phòng")]
        public bool Status { get; set; } // trạng thái phòng - trống or thuê

        [DisplayName("Diện tích phòng")]
        public float Area { get; set; }// Diện tích đơn vị là mét ( m )
        [DisplayName("Nội dung")]
        public string Description { get; set; }//Mo ta
        [DisplayName("Chiều cao phòng")]
        public float Height { get; set; } //cao đơn vị là mét ( m )
        [DisplayName("Chiều rộng")]
        public float Width { get; set; } //rộng đơn vị là mét ( m )
        [DisplayName("Loại phòng")]
        public int RoomTypeId { get; set; } //mã loại phòng
        [DisplayName("Voucher")]
        public int VoucherId { get; set; }// mã voucher

        [DisplayName("Giá Phòng")]
        public int PriceManagementId { get; set; } // mã quản lý giá 

        public IEnumerable<SelectListItem> PriceManagementList { get; set; }
        public IEnumerable<SelectListItem> RoomTypeList { get; set; }
        public IEnumerable<SelectListItem> VoucherList { get; set; }



    }
}
