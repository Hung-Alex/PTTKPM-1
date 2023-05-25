using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace RoomManagement.Areas.Admin.Models.VoucherModel
{
    public class VoucherFilterModel
    {
        [DisplayName("Từ khóa")]
        public string Name { get; set; }// tên voucher

        [DisplayName("Voucher")]
        public int? VoucherId { get; set; }
        public IEnumerable<SelectListItem> VoucherList { get; set; }
    }
}
