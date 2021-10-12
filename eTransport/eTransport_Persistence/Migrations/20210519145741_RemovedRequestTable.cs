using Microsoft.EntityFrameworkCore.Migrations;

namespace eTransport_Persistence.Migrations
{
    public partial class RemovedRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_ETransportUserId",
                table: "Requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Requests",
                table: "Requests");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "RequestDto");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_ETransportUserId",
                table: "RequestDto",
                newName: "IX_RequestDto_ETransportUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestDto",
                table: "RequestDto",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestDto_AspNetUsers_ETransportUserId",
                table: "RequestDto",
                column: "ETransportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestDto_AspNetUsers_ETransportUserId",
                table: "RequestDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestDto",
                table: "RequestDto");

            migrationBuilder.RenameTable(
                name: "RequestDto",
                newName: "Requests");

            migrationBuilder.RenameIndex(
                name: "IX_RequestDto_ETransportUserId",
                table: "Requests",
                newName: "IX_Requests_ETransportUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Requests",
                table: "Requests",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_ETransportUserId",
                table: "Requests",
                column: "ETransportUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
