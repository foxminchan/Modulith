﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modulith.Modules.Orders.Infrastructures.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Modulith.Modules.Orders.Infrastructures.Data.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20240423064122_Initialize-Database")]
    partial class InitializeDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Modulith.Modules.Orders.Domain.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Code")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("code");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 4, 23, 6, 41, 21, 892, DateTimeKind.Utc).AddTicks(214))
                        .HasColumnName("created_date");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uuid")
                        .HasColumnName("customer_id");

                    b.Property<DateTime?>("UpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 4, 23, 6, 41, 21, 893, DateTimeKind.Utc).AddTicks(1351))
                        .HasColumnName("update_date");

                    b.Property<Guid>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValue(new Guid("ad4378a2-6343-410d-8194-841af2ce8e35"))
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_order");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("Modulith.Modules.Orders.Domain.OrderItem", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("product_id");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 4, 23, 6, 41, 21, 896, DateTimeKind.Utc).AddTicks(2071))
                        .HasColumnName("created_date");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<DateTime?>("UpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 4, 23, 6, 41, 21, 896, DateTimeKind.Utc).AddTicks(2560))
                        .HasColumnName("update_date");

                    b.Property<Guid>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValue(new Guid("8a705c00-5276-4230-8f73-f5fb26f5bf8e"))
                        .HasColumnName("version");

                    b.HasKey("OrderId", "ProductId")
                        .HasName("pk_order_item");

                    b.ToTable("order_item", (string)null);
                });

            modelBuilder.Entity("Modulith.Modules.Orders.Domain.OrderItem", b =>
                {
                    b.HasOne("Modulith.Modules.Orders.Domain.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_item_order_order_id");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Modulith.Modules.Orders.Domain.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}