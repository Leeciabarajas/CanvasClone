using Microsoft.EntityFrameworkCore.Migrations;

namespace SeeSharpLMS.Migrations
{
    public partial class IntegrateIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Student_StudentId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_Instructor_InstructorId",
                table: "Section");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "StudentAssignment",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Section",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Enrollment",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignment_StudentId",
                table: "StudentAssignment",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_AspNetUsers_StudentId",
                table: "Enrollment",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_AspNetUsers_InstructorId",
                table: "Section",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignment_AspNetUsers_StudentId",
                table: "StudentAssignment",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_AspNetUsers_StudentId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Section_AspNetUsers_InstructorId",
                table: "Section");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignment_AspNetUsers_StudentId",
                table: "StudentAssignment");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssignment_StudentId",
                table: "StudentAssignment");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentAssignment");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "Section",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Enrollment",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Student_StudentId",
                table: "Enrollment",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Instructor_InstructorId",
                table: "Section",
                column: "InstructorId",
                principalTable: "Instructor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
