using Microsoft.EntityFrameworkCore.Migrations;

namespace eTransport_Persistence.Migrations
{
    public partial class Expanded_Request : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Requests");
        }
    }
}
