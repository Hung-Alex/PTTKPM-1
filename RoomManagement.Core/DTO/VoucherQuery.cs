using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Core.DTO
{
    public class VoucherQuery
    {
        public string Name { get; set; }// tên loại phòng
        public string UrlSlug { get; set; }
    }
}
