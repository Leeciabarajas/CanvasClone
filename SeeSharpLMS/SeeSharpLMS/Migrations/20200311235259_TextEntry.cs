using Microsoft.EntityFrameworkCore.Migrations;

namespace SeeSharpLMS.Migrations
{
    public partial class TextEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextEntry",
                table: "Submission",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextEntry",
                table: "Submission");
        }
    }
}
