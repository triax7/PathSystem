using Microsoft.EntityFrameworkCore.Migrations;

namespace PathSystem.Api.Migrations
{
    public partial class AddedOwnerIdPropToRoute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Owners_OwnerId",
                table: "Routes");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Owners_OwnerId",
                table: "Routes",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Owners_OwnerId",
                table: "Routes");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Routes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[] { 1, "admin@admin.admin", "Admin", "AHVwC1Gelz8mveHqFETmCHnwyoa0HFeLBkNEOn0WFnIKkNQTRAygRzrWoVmfn9me4Q==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Owners_OwnerId",
                table: "Routes",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
