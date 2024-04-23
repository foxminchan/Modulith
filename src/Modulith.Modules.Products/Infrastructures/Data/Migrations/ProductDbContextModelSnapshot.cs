﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modulith.Modules.Products.Infrastructures.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Modulith.Modules.Products.Infrastructures.Data.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    partial class ProductDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Modulith.Modules.Products.Domain.CategoryAggregate.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 4, 23, 5, 36, 53, 738, DateTimeKind.Utc).AddTicks(9557))
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 4, 23, 5, 36, 53, 740, DateTimeKind.Utc).AddTicks(432))
                        .HasColumnName("update_date");

                    b.Property<Guid>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValue(new Guid("a8615eca-bc8f-4fc2-bf7f-fffe0f630d30"))
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("9f3d6f08-33d5-4efb-91ec-3b7b42f93678"),
                            CreatedDate = new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(4451),
                            Description = "Books featuring magical or supernatural elements often set in imaginary worlds.",
                            Name = "Fantasy",
                            Version = new Guid("e921b958-ec69-4172-954d-7be5dd31552f")
                        },
                        new
                        {
                            Id = new Guid("f6bbab52-026c-4b9f-aa74-d327d39dea62"),
                            CreatedDate = new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5401),
                            Description = "Books exploring speculative concepts such as advanced science and technology, space exploration, or futuristic societies.",
                            Name = "Science Fiction",
                            Version = new Guid("7300025d-a30c-4473-ab93-d04b226da470")
                        },
                        new
                        {
                            Id = new Guid("ec20ecc2-b087-4cf8-842c-39a34e37a1b3"),
                            CreatedDate = new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5464),
                            Description = "Books centered around solving a crime or unraveling a puzzle, often featuring detectives or amateur sleuths.",
                            Name = "Mystery",
                            Version = new Guid("05af7a3f-dc4d-4631-9de8-8f0fe792fa28")
                        },
                        new
                        {
                            Id = new Guid("6308001a-a5cc-48c5-8c5a-3c15864e59a4"),
                            CreatedDate = new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5467),
                            Description = "Books focusing on romantic relationships and emotional connections between characters.",
                            Name = "Romance",
                            Version = new Guid("c322662a-32b2-4b77-be81-b4593a6f0482")
                        },
                        new
                        {
                            Id = new Guid("f1a87803-7db1-45f4-b904-94cc149a7971"),
                            CreatedDate = new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5469),
                            Description = "Books set in the past, often blending fictional characters and events with real historical contexts.",
                            Name = "Historical Fiction",
                            Version = new Guid("f7c226fb-6360-465f-8cab-2c2e8f4b7237")
                        },
                        new
                        {
                            Id = new Guid("314f0b35-abcf-454c-91fd-25c3ba1860cc"),
                            CreatedDate = new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5474),
                            Description = "Books designed to provoke excitement, tension, and suspense, often involving danger and high stakes.",
                            Name = "Thriller",
                            Version = new Guid("cc2bc086-28fe-4ff9-a437-4f8a12180729")
                        },
                        new
                        {
                            Id = new Guid("ce3bed7a-f0f1-40fd-962a-b6a8bc89301c"),
                            CreatedDate = new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5477),
                            Description = "Books intended to evoke fear, dread, or terror through supernatural or psychological elements.",
                            Name = "Horror",
                            Version = new Guid("d2ca8847-f355-4cdb-be26-843a226270d0")
                        },
                        new
                        {
                            Id = new Guid("716b64c3-4ceb-4dc3-9530-3a3df846e696"),
                            CreatedDate = new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5479),
                            Description = "Books recounting the life and experiences of a real person, often written by another individual.",
                            Name = "Biography",
                            Version = new Guid("9f0372f7-f281-4f67-8d5c-6f6e37c23757")
                        },
                        new
                        {
                            Id = new Guid("9f5579b7-291f-406a-ace6-8b21144ca02a"),
                            CreatedDate = new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5481),
                            Description = "Books offering advice, guidance, and strategies for personal growth, improvement, and self-discovery.",
                            Name = "Self-Help",
                            Version = new Guid("7e73d129-9630-4b70-8fee-995153121962")
                        },
                        new
                        {
                            Id = new Guid("0997b4e1-5458-4994-8c9e-07180fb79cfc"),
                            CreatedDate = new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5484),
                            Description = "Books providing recipes, cooking techniques, and culinary inspiration for preparing various dishes and cuisines.",
                            Name = "Cooking",
                            Version = new Guid("5e111c82-cdd0-4389-86e6-56bbe8d77a89")
                        });
                });

            modelBuilder.Entity("Modulith.Modules.Products.Domain.ProductAggregate.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 4, 23, 5, 36, 53, 744, DateTimeKind.Utc).AddTicks(4271))
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)")
                        .HasColumnName("description");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("ProductCode")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("product_code");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("quantity");

                    b.Property<int?>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime?>("UpdateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValue(new DateTime(2024, 4, 23, 5, 36, 53, 744, DateTimeKind.Utc).AddTicks(4889))
                        .HasColumnName("update_date");

                    b.Property<Guid>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValue(new Guid("18d91a08-b23a-42f2-a021-09b878cfb4f3"))
                        .HasColumnName("version");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("ix_products_category_id");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("Modulith.Modules.Products.Domain.ProductAggregate.Product", b =>
                {
                    b.HasOne("Modulith.Modules.Products.Domain.CategoryAggregate.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("fk_products_categories_category_id");

                    b.OwnsOne("Modulith.Modules.Products.Domain.ProductAggregate.ValueObjects.ProductImage", "Image", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("Alt")
                                .HasColumnType("text");

                            b1.Property<string>("ImageUrl")
                                .HasColumnType("text");

                            b1.Property<string>("Title")
                                .HasColumnType("text");

                            b1.HasKey("ProductId");

                            b1.ToTable("products");

                            b1.ToJson("image");

                            b1.WithOwner()
                                .HasForeignKey("ProductId")
                                .HasConstraintName("fk_products_products_id");
                        });

                    b.OwnsOne("Modulith.Modules.Products.Domain.ProductAggregate.ValueObjects.ProductPrice", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Price")
                                .HasColumnType("numeric");

                            b1.Property<decimal>("PriceSale")
                                .HasColumnType("numeric");

                            b1.HasKey("ProductId");

                            b1.ToTable("products");

                            b1.ToJson("price");

                            b1.WithOwner()
                                .HasForeignKey("ProductId")
                                .HasConstraintName("fk_products_products_id");
                        });

                    b.Navigation("Category");

                    b.Navigation("Image");

                    b.Navigation("Price");
                });

            modelBuilder.Entity("Modulith.Modules.Products.Domain.CategoryAggregate.Category", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
