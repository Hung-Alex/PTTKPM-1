using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomManagement.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Data.Mappings
{
    public class RoomMap : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Room");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(500);
            builder.Property(x => x.UrlSlug)
               .IsRequired()
               .HasMaxLength(500);

            builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500);

            builder.Property(x => x.Image)
               .HasMaxLength(1000);


            builder.Property(x => x.Video)               
               .HasMaxLength(1000);

            builder.Property(x => x.Status)
                .HasDefaultValue(false);

            builder.Property(x => x.Area)
                .HasColumnType("float");

            builder.Property(x => x.Height)
               .HasColumnType("float");

            builder.Property(x => x.Width)
               .HasColumnType("float");


            //cấu hình quan hệ giữa các bảng 

            //cấu hình room với roomtype
            builder
                .HasOne<RoomType>(x => x.RoomType)
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.RoomTypeId)
                .OnDelete(DeleteBehavior.Cascade);// xóa các thành phần phụ thuộc khi 1 thành phần chính bị xóa 

            //cấu hình room với PriceManagement
            builder
               .HasOne<PriceManagement>(x => x.PriceManagement)
               .WithMany(x => x.Rooms)
               .HasForeignKey(x => x.PriceManagementId)
               .OnDelete(DeleteBehavior.Cascade);// xóa các thành phần phụ thuộc khi 1 thành phần chính bị xóa

            //cấu hình room với Voucher

            builder
               .HasOne<Voucher>(x => x.Voucher)
               .WithMany(x => x.Rooms)
               .HasForeignKey(x => x.VoucherId)
               .OnDelete(DeleteBehavior.Cascade);// xóa các thành phần phụ thuộc khi 1 thành phần chính bị xóa
        }
    }
}
