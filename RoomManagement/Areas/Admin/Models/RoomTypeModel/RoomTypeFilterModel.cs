using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace RoomManagement.Areas.Admin.Models.RoomTypeModel
{
    public class RoomTypeFilterModel
    {
        [DisplayName("Từ khóa")]
        public string Name { get; set; }
        public int? RoomTypeId { get; set; }
        public IEnumerable<SelectListItem> RoomTypeList { get; set; }
    }
}
