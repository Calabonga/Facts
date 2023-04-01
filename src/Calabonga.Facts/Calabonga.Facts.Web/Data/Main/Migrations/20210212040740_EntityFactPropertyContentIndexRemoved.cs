using Microsoft.EntityFrameworkCore.Migrations;

namespace Calabonga.Facts.Web.Data.Migrations
{
    public partial class EntityFactPropertyContentIndexRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Facts_Content",
                table: "Facts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Facts_Content",
                table: "Facts",
                column: "Content");
        }
    }
}
