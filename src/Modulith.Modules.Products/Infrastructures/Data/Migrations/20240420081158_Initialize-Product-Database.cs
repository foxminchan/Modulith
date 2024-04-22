using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Modulith.Modules.Products.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitializeProductDatabase : Migration
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
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 4, 20, 8, 11, 58, 594, DateTimeKind.Utc).AddTicks(2790)),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: new DateTime(2024, 4, 20, 8, 11, 58, 595, DateTimeKind.Utc).AddTicks(5264)),
                    version = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("e5689c21-faf7-4701-b10f-42160ad12f2e"))
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
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTime(2024, 4, 20, 8, 11, 58, 610, DateTimeKind.Utc).AddTicks(1460)),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValue: new DateTime(2024, 4, 20, 8, 11, 58, 610, DateTimeKind.Utc).AddTicks(2074)),
                    version = table.Column<Guid>(type: "uuid", nullable: false, defaultValue: new Guid("2e457bbe-d46f-4318-b6d4-63dc52786d31")),
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
                    { new Guid("3aa05c8a-ad4c-4fe6-8447-ce8bb5375831"), new DateTime(2024, 4, 20, 8, 11, 58, 609, DateTimeKind.Utc).AddTicks(1216), "Books recounting the life and experiences of a real person, often written by another individual.", "Biography", new Guid("f0cbda11-100f-4c29-b704-3eaa96f1fede") },
                    { new Guid("3fdd7457-0f21-4f1b-b2d2-d35ff7d564fc"), new DateTime(2024, 4, 20, 8, 11, 58, 609, DateTimeKind.Utc).AddTicks(1183), "Books centered around solving a crime or unraveling a puzzle, often featuring detectives or amateur sleuths.", "Mystery", new Guid("f86177a3-29b8-4e49-a05f-d9359d92c8ee") },
                    { new Guid("5258d841-b9c5-417b-a7f1-cded6f945b5e"), new DateTime(2024, 4, 20, 8, 11, 58, 609, DateTimeKind.Utc).AddTicks(1122), "Books exploring speculative concepts such as advanced science and technology, space exploration, or futuristic societies.", "Science Fiction", new Guid("9cfd0048-88e0-4164-9fc8-6204d706696c") },
                    { new Guid("7957dd55-75c2-4b78-975f-bfa57413ae49"), new DateTime(2024, 4, 20, 8, 11, 58, 609, DateTimeKind.Utc).AddTicks(1210), "Books designed to provoke excitement, tension, and suspense, often involving danger and high stakes.", "Thriller", new Guid("d69b0e39-5bb9-4dbf-8de7-ec4fa4c460d3") },
                    { new Guid("86e259d9-5a7d-4475-8e5b-f3a9755e53e6"), new DateTime(2024, 4, 20, 8, 11, 58, 609, DateTimeKind.Utc).AddTicks(1221), "Books providing recipes, cooking techniques, and culinary inspiration for preparing various dishes and cuisines.", "Cooking", new Guid("1756d7e9-b926-4e5c-8702-f66bc2f2746b") },
                    { new Guid("9dfbd2c6-cfa4-4f07-8645-47640e0275fc"), new DateTime(2024, 4, 20, 8, 11, 58, 609, DateTimeKind.Utc).AddTicks(1205), "Books set in the past, often blending fictional characters and events with real historical contexts.", "Historical Fiction", new Guid("97389970-b5a0-4ced-8bad-cee883cee6cc") },
                    { new Guid("a186dbbe-143b-4a90-8bfa-5846f4d42f9f"), new DateTime(2024, 4, 20, 8, 11, 58, 609, DateTimeKind.Utc).AddTicks(1202), "Books focusing on romantic relationships and emotional connections between characters.", "Romance", new Guid("c2cf880a-8ae5-4344-9b12-571638060eb6") },
                    { new Guid("a73286d3-526a-43b0-a2c0-3bb0d687650d"), new DateTime(2024, 4, 20, 8, 11, 58, 609, DateTimeKind.Utc).AddTicks(1218), "Books offering advice, guidance, and strategies for personal growth, improvement, and self-discovery.", "Self-Help", new Guid("605dcfd3-18ee-4a1b-b308-c6fa7e724beb") },
                    { new Guid("d618f667-521c-435c-a79f-64ba6ecd4375"), new DateTime(2024, 4, 20, 8, 11, 58, 609, DateTimeKind.Utc).AddTicks(21), "Books featuring magical or supernatural elements often set in imaginary worlds.", "Fantasy", new Guid("542b5405-2f6b-486a-b49d-fde75a1c44bb") },
                    { new Guid("df7bda46-8dcd-4986-82c1-5598d19e40a7"), new DateTime(2024, 4, 20, 8, 11, 58, 609, DateTimeKind.Utc).AddTicks(1213), "Books intended to evoke fear, dread, or terror through supernatural or psychological elements.", "Horror", new Guid("256924bd-2652-4172-9f5a-498f723c011f") }
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
