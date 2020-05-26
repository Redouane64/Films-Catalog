using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FilmsLibrary.Migrations
{
    public partial class AddFilmPosterColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "image",
                schema: "catalog",
                table: "films",
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                schema: "catalog",
                table: "films");
        }
    }
}
