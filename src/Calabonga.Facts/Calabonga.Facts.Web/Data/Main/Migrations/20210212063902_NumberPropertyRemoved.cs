using Microsoft.EntityFrameworkCore.Migrations;

namespace Calabonga.Facts.Web.Data.Migrations
{
    public partial class NumberPropertyRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Facts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Facts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
