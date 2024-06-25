using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learningManagementSystem.DAL.Migrations
{
    public partial class UpdateCourseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgeUrl",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgeUrl",
                table: "Courses");
        }
    }
}
