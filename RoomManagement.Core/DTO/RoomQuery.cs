using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Core.DTO
{
    public class RoomQuery
    {
        public string Name { get; set; }// tên loại phòng
        public string UrlSlug { get; set; }
        //--------------------------------------------
        public string RoomTypeSlug{get;set;}
        public string PriceManagementSlug { get; set; }
        public string VoucherSlug { get; set; }
        public int? RoomTypeId { get; set; }
        public int? VoucherId { get; set; }
        public int? PriceManagementId { get; set; }
        //----------------------------------------------

       
        public bool Status { get; set; } // trạng thái phòng trống hay chưa , True là thuê ,ngược lại 
        public float Area { get; set; }// Diện tích đơn vị là mét ( m )
        public float Height { get; set; } //cao đơn vị là mét ( m )
        public float Width { get; set; } //rộng đơn vị là mét ( m )
    }
}
