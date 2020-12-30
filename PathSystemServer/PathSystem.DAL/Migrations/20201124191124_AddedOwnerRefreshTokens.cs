using Microsoft.EntityFrameworkCore.Migrations;

namespace PathSystem.Api.Migrations
{
    public partial class AddedOwnerRefreshTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerRefreshToken_Owners_OwnerId",
                table: "OwnerRefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnerRefreshToken",
                table: "OwnerRefreshToken");

            migrationBuilder.RenameTable(
                name: "OwnerRefreshToken",
                newName: "OwnerRefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_OwnerRefreshToken_OwnerId",
                table: "OwnerRefreshTokens",
                newName: "IX_OwnerRefreshTokens_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnerRefreshTokens",
                table: "OwnerRefreshTokens",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "ALMUpkuKKJ0YnZpeKteSJJl5vNNEZlCcRa00E7MzqcU2mzkATYv20MIZQKIcAxqFKA==");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerRefreshTokens_Owners_OwnerId",
                table: "OwnerRefreshTokens",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerRefreshTokens_Owners_OwnerId",
                table: "OwnerRefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnerRefreshTokens",
                table: "OwnerRefreshTokens");

            migrationBuilder.RenameTable(
                name: "OwnerRefreshTokens",
                newName: "OwnerRefreshToken");

            migrationBuilder.RenameIndex(
                name: "IX_OwnerRefreshTokens_OwnerId",
                table: "OwnerRefreshToken",
                newName: "IX_OwnerRefreshToken_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnerRefreshToken",
                table: "OwnerRefreshToken",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AC5kLl/VwODi1OSBnVTH/gWPXv4PHqYCAO+m0et4o4j27edQDSniZWVWkEUVK6hgnw==");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerRefreshToken_Owners_OwnerId",
                table: "OwnerRefreshToken",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
