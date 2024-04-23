using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace Modulith.Modules.Products.Infrastructures.Data.Migrations;

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
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 4, 23, 5, 36, 53, 738, DateTimeKind.Utc).AddTicks(9557)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 4, 23, 5, 36, 53, 740, DateTimeKind.Utc).AddTicks(432)),
                version = table.Column<Guid>(type: "uuid", nullable: false,
                    defaultValue: new Guid("a8615eca-bc8f-4fc2-bf7f-fffe0f630d30"))
            },
            constraints: table => { table.PrimaryKey("pk_categories", x => x.id); });

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
                created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false,
                    defaultValue: new DateTime(2024, 4, 23, 5, 36, 53, 744, DateTimeKind.Utc).AddTicks(4271)),
                update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true,
                    defaultValue: new DateTime(2024, 4, 23, 5, 36, 53, 744, DateTimeKind.Utc).AddTicks(4889)),
                version = table.Column<Guid>(type: "uuid", nullable: false,
                    defaultValue: new Guid("18d91a08-b23a-42f2-a021-09b878cfb4f3")),
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
            columns: ["id", "created_date", "description", "name", "version"],
            values: new object[,]
            {
                {
                    new Guid("0997b4e1-5458-4994-8c9e-07180fb79cfc"),
                    new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5484),
                    "Books providing recipes, cooking techniques, and culinary inspiration for preparing various dishes and cuisines.",
                    "Cooking", new Guid("5e111c82-cdd0-4389-86e6-56bbe8d77a89")
                },
                {
                    new Guid("314f0b35-abcf-454c-91fd-25c3ba1860cc"),
                    new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5474),
                    "Books designed to provoke excitement, tension, and suspense, often involving danger and high stakes.",
                    "Thriller", new Guid("cc2bc086-28fe-4ff9-a437-4f8a12180729")
                },
                {
                    new Guid("6308001a-a5cc-48c5-8c5a-3c15864e59a4"),
                    new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5467),
                    "Books focusing on romantic relationships and emotional connections between characters.", "Romance",
                    new Guid("c322662a-32b2-4b77-be81-b4593a6f0482")
                },
                {
                    new Guid("716b64c3-4ceb-4dc3-9530-3a3df846e696"),
                    new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5479),
                    "Books recounting the life and experiences of a real person, often written by another individual.",
                    "Biography", new Guid("9f0372f7-f281-4f67-8d5c-6f6e37c23757")
                },
                {
                    new Guid("9f3d6f08-33d5-4efb-91ec-3b7b42f93678"),
                    new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(4451),
                    "Books featuring magical or supernatural elements often set in imaginary worlds.", "Fantasy",
                    new Guid("e921b958-ec69-4172-954d-7be5dd31552f")
                },
                {
                    new Guid("9f5579b7-291f-406a-ace6-8b21144ca02a"),
                    new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5481),
                    "Books offering advice, guidance, and strategies for personal growth, improvement, and self-discovery.",
                    "Self-Help", new Guid("7e73d129-9630-4b70-8fee-995153121962")
                },
                {
                    new Guid("ce3bed7a-f0f1-40fd-962a-b6a8bc89301c"),
                    new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5477),
                    "Books intended to evoke fear, dread, or terror through supernatural or psychological elements.",
                    "Horror", new Guid("d2ca8847-f355-4cdb-be26-843a226270d0")
                },
                {
                    new Guid("ec20ecc2-b087-4cf8-842c-39a34e37a1b3"),
                    new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5464),
                    "Books centered around solving a crime or unraveling a puzzle, often featuring detectives or amateur sleuths.",
                    "Mystery", new Guid("05af7a3f-dc4d-4631-9de8-8f0fe792fa28")
                },
                {
                    new Guid("f1a87803-7db1-45f4-b904-94cc149a7971"),
                    new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5469),
                    "Books set in the past, often blending fictional characters and events with real historical contexts.",
                    "Historical Fiction", new Guid("f7c226fb-6360-465f-8cab-2c2e8f4b7237")
                },
                {
                    new Guid("f6bbab52-026c-4b9f-aa74-d327d39dea62"),
                    new DateTime(2024, 4, 23, 5, 36, 53, 743, DateTimeKind.Utc).AddTicks(5401),
                    "Books exploring speculative concepts such as advanced science and technology, space exploration, or futuristic societies.",
                    "Science Fiction", new Guid("7300025d-a30c-4473-ab93-d04b226da470")
                }
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