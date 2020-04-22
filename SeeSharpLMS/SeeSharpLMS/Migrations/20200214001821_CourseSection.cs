using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SeeSharpLMS.Migrations
{
    public partial class CourseSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Section",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Section",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentCap",
                table: "Section",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Section",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "Section",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<float>(
                name: "CourseFee",
                table: "Course",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "CreditHours",
                table: "Course",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Course",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "EnrollmentCap",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "CourseFee",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CreditHours",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Course");
        }
    }
}
