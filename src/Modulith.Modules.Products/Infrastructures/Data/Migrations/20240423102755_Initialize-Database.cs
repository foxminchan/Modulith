using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Modulith.Modules.Products.Infrastructures.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 4, 23, 10, 27, 55, 242, DateTimeKind.Utc).AddTicks(3065)),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: new DateTime(2024, 4, 23, 10, 27, 55, 243, DateTimeKind.Utc).AddTicks(4913)),
                    version = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("03e65255-1f6a-4ea9-8132-a90122045dd5"))
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    product_code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    status = table.Column<int>(type: "integer", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    category_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 4, 23, 10, 27, 55, 248, DateTimeKind.Utc).AddTicks(5588)),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: new DateTime(2024, 4, 23, 10, 27, 55, 248, DateTimeKind.Utc).AddTicks(6359)),
                    version = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("f1d33def-5831-4d10-8471-2af0d1129a44")),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    image = table.Column<string>(type: "jsonb", nullable: true),
                    price = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "created_date", "description", "name", "version" },
                values: new object[,]
                {
                    { new Guid("05237d85-80d1-428f-a3b3-3d9630707692"), new DateTime(2024, 4, 23, 10, 27, 55, 247, DateTimeKind.Utc).AddTicks(5530), "Books offering advice, guidance, and strategies for personal growth, improvement, and self-discovery.", "Self-Help", new Guid("5ed284d7-3180-46c4-8a49-8cf6ac4c8f2d") },
                    { new Guid("2a6e4b52-6a81-4b71-ad7f-01f4de15cd84"), new DateTime(2024, 4, 23, 10, 27, 55, 247, DateTimeKind.Utc).AddTicks(5443), "Books exploring speculative concepts such as advanced science and technology, space exploration, or futuristic societies.", "Science Fiction", new Guid("88c9f1b6-0a72-4cf1-b1cd-d6e05585459d") },
                    { new Guid("33dd7bda-2b23-47cc-bbf9-fffdc286f92c"), new DateTime(2024, 4, 23, 10, 27, 55, 247, DateTimeKind.Utc).AddTicks(5505), "Books centered around solving a crime or unraveling a puzzle, often featuring detectives or amateur sleuths.", "Mystery", new Guid("553ce211-6136-4762-b96a-3deb7ba73d50") },
                    { new Guid("39bde98d-4565-4d9c-a4a6-d0e6d24ae37f"), new DateTime(2024, 4, 23, 10, 27, 55, 247, DateTimeKind.Utc).AddTicks(5521), "Books designed to provoke excitement, tension, and suspense, often involving danger and high stakes.", "Thriller", new Guid("63cd68b4-4aee-44c0-b2d9-cbc07d3b3ceb") },
                    { new Guid("54ad2e44-2239-4782-819c-e78495029250"), new DateTime(2024, 4, 23, 10, 27, 55, 247, DateTimeKind.Utc).AddTicks(5533), "Books providing recipes, cooking techniques, and culinary inspiration for preparing various dishes and cuisines.", "Cooking", new Guid("cbd4fa8c-51f0-40f3-8f0e-cc1de656b454") },
                    { new Guid("5c4f30d3-6330-4470-b6a3-a01261b33e46"), new DateTime(2024, 4, 23, 10, 27, 55, 247, DateTimeKind.Utc).AddTicks(4409), "Books featuring magical or supernatural elements often set in imaginary worlds.", "Fantasy", new Guid("20f7f85b-f4e7-46fb-9804-ba999e510e57") },
                    { new Guid("5edd6b26-67d1-45c1-9857-e2a3b7dfce82"), new DateTime(2024, 4, 23, 10, 27, 55, 247, DateTimeKind.Utc).AddTicks(5523), "Books intended to evoke fear, dread, or terror through supernatural or psychological elements.", "Horror", new Guid("b859aa05-57f9-4b11-bdfe-a66944cd25d1") },
                    { new Guid("806bfc07-aabc-4a37-a892-0c811f3dc09c"), new DateTime(2024, 4, 23, 10, 27, 55, 247, DateTimeKind.Utc).AddTicks(5508), "Books focusing on romantic relationships and emotional connections between characters.", "Romance", new Guid("a42b0c3f-a0d3-45cf-9804-7fb54647f086") },
                    { new Guid("9d618fe1-8ba6-4801-8f71-3349b7966428"), new DateTime(2024, 4, 23, 10, 27, 55, 247, DateTimeKind.Utc).AddTicks(5515), "Books set in the past, often blending fictional characters and events with real historical contexts.", "Historical Fiction", new Guid("9ff398f0-1468-490e-9da9-193b69c6c6ee") },
                    { new Guid("a3285a3f-a7c4-4edc-8ef9-26627f9ac5b0"), new DateTime(2024, 4, 23, 10, 27, 55, 247, DateTimeKind.Utc).AddTicks(5526), "Books recounting the life and experiences of a real person, often written by another individual.", "Biography", new Guid("fab13603-1716-4b2c-a66e-5937ce5a16c6") }
                });

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                table: "products",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
