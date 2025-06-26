using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "modell",
                columns: table => new
                {
                    IdModel = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "text", nullable: false),
                    Kategory = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__modell__C2F00099D25283D9", x => x.IdModel);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__payment__3214EC0725F1B4D8", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "school",
                columns: table => new
                {
                    IdSchool = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    addressSchool = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__school__B54450856C7F6819", x => x.IdSchool);
                });

            migrationBuilder.CreateTable(
                name: "detailingModels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdModel = table.Column<int>(type: "int", nullable: false),
                    size = table.Column<int>(type: "int", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detailingModels", x => x.id);
                    table.ForeignKey(
                        name: "FK__detailing__IdMod__3A4CA8FD",
                        column: x => x.IdModel,
                        principalTable: "modell",
                        principalColumn: "IdModel");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    IdOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSchool = table.Column<int>(type: "int", nullable: true),
                    Contact = table.Column<string>(name: "Contact ", type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PhoneContact = table.Column<string>(name: "PhoneContact ", type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProvisionAddress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfOrdder = table.Column<DateTime>(type: "date", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "date", nullable: false),
                    CostPrice = table.Column<int>(name: "CostPrice ", type: "int", nullable: false),
                    SchoolName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__C38F3009EE80D13A", x => x.IdOrder);
                    table.ForeignKey(
                        name: "FK__Order__IdSchool__6FE99F9F",
                        column: x => x.IdSchool,
                        principalTable: "school",
                        principalColumn: "IdSchool");
                });

            migrationBuilder.CreateTable(
                name: "detailingOrders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOrder = table.Column<int>(type: "int", nullable: false),
                    IdModel = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detailingOrders", x => x.id);
                    table.ForeignKey(
                        name: "FK__detailing__IdOrd__70DDC3D8",
                        column: x => x.IdOrder,
                        principalTable: "Order",
                        principalColumn: "IdOrder");
                });

            migrationBuilder.CreateIndex(
                name: "IX_detailingModels_IdModel",
                table: "detailingModels",
                column: "IdModel");

            migrationBuilder.CreateIndex(
                name: "IX_detailingOrders_IdOrder",
                table: "detailingOrders",
                column: "IdOrder");

            migrationBuilder.CreateIndex(
                name: "IX_Order_IdSchool",
                table: "Order",
                column: "IdSchool");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detailingModels");

            migrationBuilder.DropTable(
                name: "detailingOrders");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "modell");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "school");
        }
    }
}
