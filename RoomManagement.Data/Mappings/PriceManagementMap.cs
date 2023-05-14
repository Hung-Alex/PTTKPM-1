﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RoomManagement.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement.Data.Mappings
{
    public class PriceManagementMap : IEntityTypeConfiguration<PriceManagement>
    {
        public void Configure(EntityTypeBuilder<PriceManagement> builder)
        {
            builder.ToTable("PriceManagement");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(500);
            builder.Property(x => x.UrlSlug)
               .IsRequired()
               .HasMaxLength(500);

            builder.Property(x => x.Price)
               .IsRequired();
               

            

            builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500);

        }
    }
}
