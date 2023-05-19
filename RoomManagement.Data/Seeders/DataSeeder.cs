using RoomManagement.Core.Entites;
using RoomManagement.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Data.Seeders
{
    public class DataSeeder : IDataSeeder
    {
        private readonly RoomManagementDbContext _dbContext;
        public DataSeeder(RoomManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {



        }

        //public string Name { get; set; }// tên loại phòng // phong vip  url slug=> phong-vip
        //public string UrlSlug { get; set; }
        //public string Description { get; set; }// Mô tả 
        public  IList<RoomType> AddRoomType()
        {
            var roomTypes = new List<RoomType>()
            {
                new()
                {
                    Name="Phòng vip",
                    UrlSlug="phong-vip",
                    Description="Phòng vip sở hữu diện tích phòng cực rộng với đầy đủ tiện nghi như một căn nhà ở " +
                    "và có view nhìn toàn cảnh từ cửa sổ hoặc ban công ra thành phố cực đẹp."
                },
                new()
                {
                    Name="Phòng tiêu chuẩn",
                    UrlSlug="phong-tieu-chuan",
                    Description="Phòng tiêu chuẩn trong khách sạn, là loại phòng đơn giản nhất với những trang bị tối thiểu, " +
                    "có diện tích nhỏ, ở tầng thấp, không có view hoặc view không đẹp. "
                },
                new()
                {
                    Name="Phòng gia đình",
                    UrlSlug="phong-gia-dinh",
                    Description="Phòng gia đình dành cho gia đình khoảng 5-6 người" +
                    "có diện tích rộng, cách bày trí đẹp mắt, có cửa nối giúp khách có thể di chuyển qua lại giữa các phòng "

                },
                new()
                {
                    Name="Phòng cao cấp",
                    UrlSlug="phong-cao-cap",
                    Description="Phòng cao cấp giống với phòng tiêu chuẩn nhưng chất lượng hơn" +
                    "diện tích phòng được tăng thêm, có view nhìn và cách bày trí đẹp mắt hơn"
                },
                new()
                {
                    Name="Phòng căn hộ",
                    UrlSlug="phong-can-ho",
                    Description="Phòng căn hộ là phòng phục vụ những vị khách lưu trú dài ngày. " +
                    "Trang bị các tiện nghi như: bếp, dụng cụ nhà bếp, máy sấy, máy giặt… " +
                    "Dịch vụ buồng phòng thường được cung cấp 1 lần một tuần hoặc 2 lần một tuần. "
                },
                new()
                {
                    Name="Phòng hồ bơi",
                    UrlSlug="phong-ho-boi",
                    Description="Phòng hồ bơi thường liền kề với bể bơi hoặc có một hồ bơi riêng gắn liền với phòng," +
                    "mang lại sự riêng tư và không gian rộng rãi hơn cho khách hàng."

                },
            

            };

            _dbContext.AddRange(roomTypes);
            _dbContext.SaveChanges();
            return roomTypes;

        }


        //public string Name { get; set; } //ten voucher
        //public string UrlSlug { get; set; }
        //public int Discount { get; set; } // phan tram giam gia

        //public string Description { get; set; }// Mô tả 
        public IList<Voucher> AddVouchers()
        {
            var vouchers = new List<Voucher>()
            {
                new()
                {
                    Name="Ngày 30-4",
                    UrlSlug="ngay-30-4",
                    Discount=25,
                    Description="Nhan ngay 30-4 chung toi xin mo su kien nhan Voucher gia 25% giam gia phong cho toan bo quy khack tham gia su kien."
                },
                new()
                {
                    Name="Ngày Tet Nguyen Dan 10-2",
                    UrlSlug="ngay-tet-nguyen-dam-10-2",
                    Discount=30,
                    Description="Nhan ngay Tet Nguyen Dan 10-2 chung toi xin tran trong mo su kien nhan Voucher Khai Bung Ngay Tet uu dai gia tri len toi 30% chi trong ngay 10-2"
                },
                new()
                {
                    Name="Ngày Valentine 14-2",
                    UrlSlug="ngay-valentine-14-2",
                    Discount=25,
                    Description="Nhan ngay le tinh nhan Valentine 14-2 chung toi xin tran trong mo su kien cho cac cap doi de nhan Voucher Valentine uu dai len toi 25% cho cac cap doi"
                },
                 new()
                {
                    Name="Ngày Halloween 31-10",
                    UrlSlug="ngay-halloween-31-10",
                    Discount=15,
                    Description="Nhan ngay Halloween 31-10 chung toi xin tran trong mo su kien nhan Voucher cho cac cap doi len toi 15%"
                },
                  new()
                {
                    Name="Ngày Giang Sinh 25-12",
                    UrlSlug="ngay-giang-sinh",
                    Discount=25,
                    Description="Khong khi ngay le Giang Sinh dang toi nen chung toi mo su kien de nhan Voucher co gia tri len toi 25% chi trong ngay 25-12 hay tham gia cung chung toi "
                },
                   new()
                {
                    Name="Ngày Festival hoa Da Lat 11-11",
                    UrlSlug="ngay-festival-hoa-da-lat-11-11",
                    Discount=35,
                    Description="Nhan ngay Festival Hoa dang toi chung toi se khai mo su kien uu dai de nhan ngay Voucher len toi 35% trong ngay nay"
                },


            };

            _dbContext.AddRange(vouchers);
            _dbContext.SaveChanges();
            return vouchers;

        }


    }
}
