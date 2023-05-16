using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace RoomManagement.Areas.Admin.Models.RoomTypeModel
{
    public class RoomTypeEditModel
    {
        public int Id { get; set; }
        [DisplayName("Tên loại phòng")]
        public string Name { get; set; }// tên loại phòng
        [DisplayName("Slug")]
        [Remote("VerifyRoomTypeSlug", "RoomType", "Admin", HttpMethod = "POST", AdditionalFields = "Id")]
        public string UrlSlug { get; set; }

        [DisplayName("Nội dung")]
        public string Description { get; set; }// Mô tả 
    }
}
