using Microsoft.EntityFrameworkCore;
using RoomManagement.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Data.Context
{
    public class RoomManagementDbContext : DbContext
    {


        public RoomManagementDbContext(DbContextOptions<RoomManagementDbContext> options) : base(options)
        {
        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<PriceManagement> PriceManagements { get; set; }
        public DbSet<Voucher> Voucher { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }


}
