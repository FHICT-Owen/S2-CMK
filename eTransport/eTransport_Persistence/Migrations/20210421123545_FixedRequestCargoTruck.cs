using Microsoft.EntityFrameworkCore.Migrations;

namespace eTransport_Persistence.Migrations
{
    public partial class FixedRequestCargoTruck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestIds",
                table: "RideShares",
                newName: "Requests");

            migrationBuilder.AddColumn<bool>(
                name: "InUse",
                table: "Trucks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLinked",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InUse",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "IsLinked",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "Requests",
                table: "RideShares",
                newName: "RequestIds");
        }
    }
}
