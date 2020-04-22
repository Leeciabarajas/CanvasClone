using Microsoft.EntityFrameworkCore.Migrations;

namespace SeeSharpLMS.Migrations
{
    public partial class flag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Flag",
                table: "Assignment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "NotificationUpdateds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionId = table.Column<int>(nullable: false),
                    CheckPoint = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUpdateds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationUpdateds_Submission_SubmissionId",
                        column: x => x.SubmissionId,
                        principalTable: "Submission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUpdateds_SubmissionId",
                table: "NotificationUpdateds",
                column: "SubmissionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationUpdateds");

            migrationBuilder.DropColumn(
                name: "Flag",
                table: "Assignment");
        }
    }
}
