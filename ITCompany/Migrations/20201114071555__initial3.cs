using Microsoft.EntityFrameworkCore.Migrations;

namespace ITCompany.Migrations
{
    public partial class _initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsUsers",
                table: "EventsUsers",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsUsers",
                table: "EventsUsers");
        }
    }
}
