using Microsoft.EntityFrameworkCore.Migrations;

namespace SeeSharpLMS.Migrations
{
    public partial class Restructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Block",
                table: "Term",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Term",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignmentTypeId",
                table: "Assignment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AssignmentType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_AssignmentTypeId",
                table: "Assignment",
                column: "AssignmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_AssignmentType_AssignmentTypeId",
                table: "Assignment",
                column: "AssignmentTypeId",
                principalTable: "AssignmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_AssignmentType_AssignmentTypeId",
                table: "Assignment");

            migrationBuilder.DropTable(
                name: "AssignmentType");

            migrationBuilder.DropIndex(
                name: "IX_Assignment_AssignmentTypeId",
                table: "Assignment");

            migrationBuilder.DropColumn(
                name: "Block",
                table: "Term");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Term");

            migrationBuilder.DropColumn(
                name: "AssignmentTypeId",
                table: "Assignment");
        }
    }
}
