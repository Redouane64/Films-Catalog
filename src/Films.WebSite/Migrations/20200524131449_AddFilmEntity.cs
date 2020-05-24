using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsLibrary.Migrations
{
    public partial class AddFilmEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.CreateTable(
                name: "films",
                schema: "catalog",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    year = table.Column<short>(type: "smallint", nullable: false),
                    director = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_films", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "films",
                schema: "catalog");
        }
    }
}
