using Microsoft.EntityFrameworkCore.Migrations;

namespace SeeSharpLMS.Migrations
{
    public partial class updateNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Submission_SubmissionId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_SubmissionId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "SubmissionId",
                table: "Notification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubmissionId",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_SubmissionId",
                table: "Notification",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Submission_SubmissionId",
                table: "Notification",
                column: "SubmissionId",
                principalTable: "Submission",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
