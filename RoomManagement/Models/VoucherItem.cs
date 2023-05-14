namespace RoomManagement.Models
{
    public class VoucherItem
    {
        public int Id { get; set; }
        public string Name { get; set; } //ten voucher
        public string UrlSlug { get; set; }
        public int Discount { get; set; } // phan tram giam gia

        public string Description { get; set; }// Mô tả 
    }
}
