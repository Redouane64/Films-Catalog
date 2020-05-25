using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsLibrary.Migrations
{
    public partial class FixIdentityUserClaimsTableSchemaAndIgnoreUserPhoneNumberColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "catalog",
                table: "users");

            migrationBuilder.RenameTable(
                name: "user_claims",
                newName: "user_claims",
                newSchema: "catalog");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "user_claims",
                schema: "catalog",
                newName: "user_claims");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "catalog",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
