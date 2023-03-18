using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Luftborn.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deposit = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    AmountAvailable = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_User_SellerId",
                        column: x => x.SellerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name" },
                values: new object[] { 1L, new DateTime(2023, 3, 18, 20, 55, 55, 482, DateTimeKind.Local).AddTicks(151), false, "Seller" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "Password", "RoleId" },
                values: new object[] { 1L, new DateTime(2023, 3, 18, 20, 55, 55, 487, DateTimeKind.Local).AddTicks(2844), false, "Seller One", null, 1L });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "Password", "RoleId" },
                values: new object[] { 2L, new DateTime(2023, 3, 18, 20, 55, 55, 487, DateTimeKind.Local).AddTicks(3246), false, "Seller Two", null, 1L });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "AmountAvailable", "Cost", "CreatedAt", "IsDeleted", "Name", "SellerId" },
                values: new object[,]
                {
                    { 1L, 10, 10, new DateTime(2023, 3, 18, 20, 55, 55, 488, DateTimeKind.Local).AddTicks(1493), false, "Product One", 1L },
                    { 2L, 10, 20, new DateTime(2023, 3, 18, 20, 55, 55, 488, DateTimeKind.Local).AddTicks(1794), false, "Product Two", 1L },
                    { 3L, 10, 30, new DateTime(2023, 3, 18, 20, 55, 55, 488, DateTimeKind.Local).AddTicks(1829), false, "Product Three", 1L },
                    { 4L, 10, 40, new DateTime(2023, 3, 18, 20, 55, 55, 488, DateTimeKind.Local).AddTicks(1853), false, "Product Four", 1L },
                    { 5L, 10, 50, new DateTime(2023, 3, 18, 20, 55, 55, 488, DateTimeKind.Local).AddTicks(1875), false, "Product Five", 1L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_SellerId",
                table: "Product",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                table: "User",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
