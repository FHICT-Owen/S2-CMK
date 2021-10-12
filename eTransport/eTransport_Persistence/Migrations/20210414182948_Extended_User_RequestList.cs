using Microsoft.EntityFrameworkCore.Migrations;

namespace eTransport_Persistence.Migrations
{
    public partial class Extended_User_RequestList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Materials = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialType = table.Column<int>(type: "int", nullable: false),
                    CargoHeight = table.Column<float>(type: "real", nullable: false),
                    CargoWidth = table.Column<float>(type: "real", nullable: false),
                    CargoLength = table.Column<float>(type: "real", nullable: false),
                    CargoVolume = table.Column<float>(type: "real", nullable: false),
                    PickupLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETransportUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_ETransportUserId",
                        column: x => x.ETransportUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ETransportUserId",
                table: "Requests",
                column: "ETransportUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
