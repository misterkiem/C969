using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentsManager.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    countryId = table.Column<int>(type: "INT(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    country = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    createdBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastUpdate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    lastUpdateBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.countryId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    userId = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userName = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    active = table.Column<sbyte>(type: "TINYINT(1)", nullable: false),
                    createDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    createdBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastUpdate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    lastUpdateBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.userId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    cityId = table.Column<int>(type: "INT(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    city = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    countryId = table.Column<int>(type: "INT(10)", nullable: false),
                    createDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    createdBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastUpdate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    lastUpdateBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.cityId);
                    table.ForeignKey(
                        name: "FK_city_country_countryId",
                        column: x => x.countryId,
                        principalTable: "country",
                        principalColumn: "countryId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    addressId = table.Column<int>(type: "INT(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    address = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address2 = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cityId = table.Column<int>(type: "INT(10)", nullable: false),
                    postalCode = table.Column<string>(type: "VARCHAR(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "VARCHAR(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    createdBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastUpdate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    lastUpdateBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.addressId);
                    table.ForeignKey(
                        name: "FK_address_city_cityId",
                        column: x => x.cityId,
                        principalTable: "city",
                        principalColumn: "cityId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customerId = table.Column<int>(type: "INT(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customerName = table.Column<string>(type: "VARCHAR(45)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    addressId = table.Column<int>(type: "INT(10)", nullable: false),
                    active = table.Column<sbyte>(type: "TINYINT", nullable: false),
                    createDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    createdBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastUpdate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    lastUpdateBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.customerId);
                    table.ForeignKey(
                        name: "FK_customer_address_addressId",
                        column: x => x.addressId,
                        principalTable: "address",
                        principalColumn: "addressId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "appointment",
                columns: table => new
                {
                    appointmentId = table.Column<int>(type: "INT(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    customerId = table.Column<int>(type: "INT(10)", nullable: false),
                    userId = table.Column<int>(type: "INT", nullable: false),
                    title = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "TEXT", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    location = table.Column<string>(type: "TEXT", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contact = table.Column<string>(type: "TEXT", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    type = table.Column<string>(type: "TEXT", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    url = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    start = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    end = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    createDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    createdBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastUpdate = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    lastUpdateBy = table.Column<string>(type: "VARCHAR(40)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment", x => x.appointmentId);
                    table.ForeignKey(
                        name: "FK_appointment_customer_customerId",
                        column: x => x.customerId,
                        principalTable: "customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointment_user_userId",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_address_cityId",
                table: "address",
                column: "cityId");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_customerId",
                table: "appointment",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_appointment_userId",
                table: "appointment",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_city_countryId",
                table: "city",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_customer_addressId",
                table: "customer",
                column: "addressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointment");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
