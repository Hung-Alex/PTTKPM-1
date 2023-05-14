namespace RoomManagement.Models
{
    public class PriceManagementItem
    {
        public int Id { get; set; }
        public string Name { get; set; }// tên của loại giá ngày lễ giá ngày thường 
        public string UrlSlug { get; set; }
        public int Price { get; set; } //giá tiền 
        public string Description { get; set; }// Mô tả 
    }
}
