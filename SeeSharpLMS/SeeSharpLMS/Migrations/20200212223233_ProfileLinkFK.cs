using Microsoft.EntityFrameworkCore.Migrations;

namespace SeeSharpLMS.Migrations
{
    public partial class ProfileLinkFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "ProfileLink",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfileLink_InstructorId",
                table: "ProfileLink",
                column: "InstructorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileLink_AspNetUsers_InstructorId",
                table: "ProfileLink",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileLink_AspNetUsers_InstructorId",
                table: "ProfileLink");

            migrationBuilder.DropIndex(
                name: "IX_ProfileLink_InstructorId",
                table: "ProfileLink");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "ProfileLink",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
