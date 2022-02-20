﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NSE.Cart.API.Data;

namespace NSE.Cart.API.Migrations
{
    [DbContext(typeof(CartContext))]
    partial class CartContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NSE.Cart.API.Model.CartCustomer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("VoucherUsed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .HasName("IDX_Customer");

                    b.ToTable("CartCustomer");
                });

            modelBuilder.Entity("NSE.Cart.API.Model.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("NSE.Cart.API.Model.CartCustomer", b =>
                {
                    b.OwnsOne("NSE.Cart.API.Model.Voucher", "Voucher", b1 =>
                        {
                            b1.Property<Guid>("CartCustomerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Code")
                                .HasColumnName("VoucherCode")
                                .HasColumnType("varchar(50)");

                            b1.Property<decimal?>("Percentage")
                                .HasColumnName("Percentage")
                                .HasColumnType("decimal(18,2)");

                            b1.Property<int>("TypeDiscount")
                                .HasColumnName("TypeDiscount")
                                .HasColumnType("int");

                            b1.Property<decimal?>("ValueDiscount")
                                .HasColumnName("ValueDiscount")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("CartCustomerId");

                            b1.ToTable("CartCustomer");

                            b1.WithOwner()
                                .HasForeignKey("CartCustomerId");
                        });
                });

            modelBuilder.Entity("NSE.Cart.API.Model.CartItem", b =>
                {
                    b.HasOne("NSE.Cart.API.Model.CartCustomer", "CartCustomer")
                        .WithMany("Itens")
                        .HasForeignKey("CartId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
