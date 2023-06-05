using Microsoft.AspNetCore.Mvc.Rendering;
using RoomManagement.Core.Contracts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoomManagement.Areas.Admin.Models.RoomModel
{
    public class RoomFilterModel: IPageQuery
    {
        [DisplayName("Từ Khóa")]
        public string Name { get; set; }// tên loại phòng
        //public string UrlSlug { get; set; }
        ////--------------------------------------------
        //public string RoomTypeSlug { get; set; }
        //public string PriceManagementSlug { get; set; }
        //public string VoucherSlug { get; set; }

        [DisplayName("Loại phòng ")]
        public int? RoomTypeId { get; set; }
        [DisplayName("Voucher ")]
        public int? VoucherId { get; set; }
        [DisplayName("Loại giá")]
        public int? PriceManagementId { get; set; }
        //----------------------------------------------

        public int Price { get; set; }//giá phòng

        [DisplayName("Giá thấp nhất")]
        public int? MinPrice { get; set; }
        [DisplayName("Giá cao nhất")]
        public int? MaxPrice { get; set; }

        [DisplayName("Trạng thái phòng")]
        public bool? Status { get; set; } // trạng thái phòng trống hay chưa , True là thuê ,ngược lại 

        public IEnumerable<SelectListItem> PriceManagementList { get; set; }
        public IEnumerable<SelectListItem> RoomTypeList { get; set; }
        public IEnumerable<SelectListItem> VoucherList { get; set; }



        public string CreateQuery()
        {
           
            return $"Name={Name}&RoomTypeId={RoomTypeId}&VoucherId={VoucherId}&PriceManagementId={PriceManagementId}&Status={Status}&MaxPrice={MaxPrice}&MinPrice={MinPrice}";
        }

        //public float? Area { get; set; }// Diện tích đơn vị là mét ( m )
        //public float? Height { get; set; } //cao đơn vị là mét ( m )
        //public float? Width { get; set; } //rộng đơn vị là mét ( m )
    }
}
