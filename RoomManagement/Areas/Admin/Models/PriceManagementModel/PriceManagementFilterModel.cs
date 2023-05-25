using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoomManagement.Areas.Admin.Models.PriceManagementModel
{
    public class PriceManagementFilterModel
    {
        [DisplayName("Từ khóa")]
        public string Name { get; set; }// tên của loại giá ngày lễ giá ngày thường 
        public IEnumerable<SelectListItem> PriceManagementList { get; set; }
    }
}
