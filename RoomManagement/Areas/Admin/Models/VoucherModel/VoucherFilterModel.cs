using System.ComponentModel;

namespace RoomManagement.Areas.Admin.Models.VoucherModel
{
    public class VoucherFilterModel
    {
        [DisplayName("Từ khóa")]
        public string Name { get; set; }// tên voucher
    }
}
