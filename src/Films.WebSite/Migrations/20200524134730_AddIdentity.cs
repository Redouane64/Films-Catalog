using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsLibrary.Migrations
{
    public partial class AddIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "creator_id",
                schema: "catalog",
                table: "films",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "users",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_films_creator_id",
                schema: "catalog",
                table: "films",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "catalog",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "catalog",
                table: "users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_films_users_creator_id",
                schema: "catalog",
                table: "films",
                column: "creator_id",
                principalSchema: "catalog",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_films_users_creator_id",
                schema: "catalog",
                table: "films");

            migrationBuilder.DropTable(
                name: "users",
                schema: "catalog");

            migrationBuilder.DropIndex(
                name: "IX_films_creator_id",
                schema: "catalog",
                table: "films");

            migrationBuilder.DropColumn(
                name: "creator_id",
                schema: "catalog",
                table: "films");
        }
    }
}
