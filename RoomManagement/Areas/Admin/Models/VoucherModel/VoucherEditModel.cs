using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace RoomManagement.Areas.Admin.Models.VoucherModel
{
    public class VoucherEditModel
    {
        public int Id { get; set; }
        [DisplayName("Tên giảm giá")]
        public string Name { get; set; } //ten voucher
        [DisplayName("Slug")]
        [Remote("VerifyVoucherSlug", "Voucher", "Admin", HttpMethod = "POST", AdditionalFields = "Id")]
        public string UrlSlug { get; set; }
        [DisplayName("Phần trăm giảm giá")]
        public int Discount { get; set; } // phan tram giam gia
        [DisplayName("Nội dung")]
        public string Description { get; set; }// Mô tả 
    }
}
