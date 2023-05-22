using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyMate.Data.Migrations
{
    public partial class BioInfoAndCourseOfStudyAddedToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_ProfilePicture_ProfilePictureId",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePictureId",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "User",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BioInfo",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseOfStudy",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_ProfilePicture_ProfilePictureId",
                table: "User",
                column: "ProfilePictureId",
                principalTable: "ProfilePicture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_ProfilePicture_ProfilePictureId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BioInfo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CourseOfStudy",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "ProfilePictureId",
                table: "User",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "User",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.AddForeignKey(
                name: "FK_User_ProfilePicture_ProfilePictureId",
                table: "User",
                column: "ProfilePictureId",
                principalTable: "ProfilePicture",
                principalColumn: "Id");
        }
    }
}
