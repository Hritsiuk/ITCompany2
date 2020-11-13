using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITCompany.Migrations
{
    public partial class _initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "EventsUsers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "EventsUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "EventsUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "EventsUsers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
