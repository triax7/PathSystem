using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace PathSystem.Api.Migrations
{
    public partial class AddedOwnersRoutesPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerRefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(nullable: true),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerRefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OwnerRefreshToken_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PathPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Point = table.Column<Point>(nullable: true),
                    RouteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PathPoints_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoute",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    RouteId = table.Column<int>(nullable: true),
                    Started = table.Column<DateTime>(nullable: false),
                    Finished = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoute_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRoute_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserPathPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    PathPointId = table.Column<int>(nullable: true),
                    Visited = table.Column<bool>(nullable: false),
                    DateVisited = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPathPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPathPoints_PathPoints_PathPointId",
                        column: x => x.PathPointId,
                        principalTable: "PathPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPathPoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AC5kLl/VwODi1OSBnVTH/gWPXv4PHqYCAO+m0et4o4j27edQDSniZWVWkEUVK6hgnw==");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerRefreshToken_OwnerId",
                table: "OwnerRefreshToken",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PathPoints_RouteId",
                table: "PathPoints",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_OwnerId",
                table: "Routes",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPathPoints_PathPointId",
                table: "UserPathPoints",
                column: "PathPointId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPathPoints_UserId",
                table: "UserPathPoints",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoute_RouteId",
                table: "UserRoute",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoute_UserId",
                table: "UserRoute",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OwnerRefreshToken");

            migrationBuilder.DropTable(
                name: "UserPathPoints");

            migrationBuilder.DropTable(
                name: "UserRoute");

            migrationBuilder.DropTable(
                name: "PathPoints");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Role" },
                values: new object[] { "AJSWNZgN7B8rWBVsv2OG2ppoWs8enKca6kAnMg49Uq8IC2O6ZUeDiFqP51VZTCBySQ==", 2 });
        }
    }
}
