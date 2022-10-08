﻿// <auto-generated />
using System;
using LionCbdShop.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LionCbdShop.Persistence.Migrations
{
    [DbContext(typeof(LionCbdShopDbContext))]
    partial class LionCbdShopDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LionCbdShop.Persistence.Entities.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("LionCbdShop.Persistence.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerProviderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdInCustomerProviderSystem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerProviderId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("LionCbdShop.Persistence.Entities.CustomerProvider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("CustomerProviders");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e379e014-121e-4a94-8e25-05d1e3d60011"),
                            Name = "Telegram"
                        });
                });

            modelBuilder.Entity("LionCbdShop.Persistence.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OrderNumber")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LionCbdShop.Persistence.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("OriginalPrice")
                        .HasColumnType("float");

                    b.Property<double?>("PriceWithDiscount")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LionCbdShop.Persistence.Entities.CartItem", b =>
                {
                    b.HasOne("LionCbdShop.Persistence.Entities.Order", null)
                        .WithMany("CartItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("LionCbdShop.Persistence.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LionCbdShop.Persistence.Entities.Customer", b =>
                {
                    b.HasOne("LionCbdShop.Persistence.Entities.CustomerProvider", "CustomerProvider")
                        .WithMany()
                        .HasForeignKey("CustomerProviderId");

                    b.Navigation("CustomerProvider");
                });

            modelBuilder.Entity("LionCbdShop.Persistence.Entities.Order", b =>
                {
                    b.HasOne("LionCbdShop.Persistence.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LionCbdShop.Persistence.Entities.Order", b =>
                {
                    b.Navigation("CartItems");
                });
#pragma warning restore 612, 618
        }
    }
}
