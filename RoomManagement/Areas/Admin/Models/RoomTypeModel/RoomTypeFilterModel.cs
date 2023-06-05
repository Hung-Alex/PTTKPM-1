using Microsoft.AspNetCore.Mvc.Rendering;
using RoomManagement.Core.Contracts;
using System.ComponentModel;

namespace RoomManagement.Areas.Admin.Models.RoomTypeModel
{
    public class RoomTypeFilterModel : IPageQuery
    {
        [DisplayName("Từ khóa")]
        public string Name { get; set; }
        public int? RoomTypeId { get; set; }
        public IEnumerable<SelectListItem> RoomTypeList { get; set; }

        public string CreateQuery()
        {
            return $"Name={Name}";
        }
    }
}
