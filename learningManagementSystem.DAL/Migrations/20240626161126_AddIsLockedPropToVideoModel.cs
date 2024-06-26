using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace learningManagementSystem.DAL.Migrations
{
    public partial class AddIsLockedPropToVideoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLocked",
                table: "Videos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLocked",
                table: "Videos");
        }
    }
}
