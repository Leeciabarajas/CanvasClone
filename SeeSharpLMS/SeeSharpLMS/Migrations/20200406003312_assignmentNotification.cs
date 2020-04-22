using Microsoft.EntityFrameworkCore.Migrations;

namespace SeeSharpLMS.Migrations
{
    public partial class assignmentNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "Notification",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AssignmentId",
                table: "Notification",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Assignment_AssignmentId",
                table: "Notification",
                column: "AssignmentId",
                principalTable: "Assignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Assignment_AssignmentId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_AssignmentId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "Notification");
        }
    }
}
