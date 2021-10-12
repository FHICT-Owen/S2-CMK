using Microsoft.EntityFrameworkCore.Migrations;

namespace eTransport_Persistence.Migrations
{
    public partial class NewUserIdInCargo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cargos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cargos");
        }
    }
}
