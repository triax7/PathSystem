using Microsoft.EntityFrameworkCore.Migrations;

namespace PathSystemServer.Migrations
{
    public partial class AddNameToRouteAndPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Routes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PathPoints",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AHVwC1Gelz8mveHqFETmCHnwyoa0HFeLBkNEOn0WFnIKkNQTRAygRzrWoVmfn9me4Q==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PathPoints");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "ALMUpkuKKJ0YnZpeKteSJJl5vNNEZlCcRa00E7MzqcU2mzkATYv20MIZQKIcAxqFKA==");
        }
    }
}
