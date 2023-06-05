using Microsoft.AspNetCore.Mvc;
using RoomManagement.Core.Contracts;
using System.ComponentModel;

namespace RoomManagement.Areas.Admin.Models.PriceManagementModel
{
    public class PriceManagementEditModel:IPageQuery
    {
        public int Id { get; set; }

        [DisplayName("Tên loại giá")]
        public string Name { get; set; }// tên của loại giá ngày lễ giá ngày thường 

        [DisplayName("Slug")]
        [Remote("VerifyPriceManagementSlug", "PriceManagement", "Admin", HttpMethod = "POST", AdditionalFields = "Id")]
        public string UrlSlug { get; set; }

        [DisplayName("Giá")]
        public int Price { get; set; } //giá tiền 

        [DisplayName("Nội dung")]
        public string Description { get; set; }// Mô tả 

        public string CreateQuery()
        {
            return $"Name={Name}";
        }
    }
}
