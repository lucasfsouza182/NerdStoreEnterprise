﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Customers.API.Models;

namespace NSE.Customers.API.Data.Mappings
{
    public class AddressMappings : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.PostalCode)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.AddressDetail)
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Neighborhood)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.City)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.State)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Addresses");
        }
    }
}