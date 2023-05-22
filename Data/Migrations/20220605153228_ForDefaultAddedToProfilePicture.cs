using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyMate.Data.Migrations
{
    public partial class ForDefaultAddedToProfilePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ForDefault",
                table: "ProfilePicture",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForDefault",
                table: "ProfilePicture");
        }
    }
}
