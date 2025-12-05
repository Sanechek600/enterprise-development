using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeRental.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BikeModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    WheelSize = table.Column<int>(type: "int", nullable: false),
                    MaxRiderWeight = table.Column<double>(type: "float", nullable: false),
                    BikeWeight = table.Column<double>(type: "float", nullable: true),
                    BrakeType = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    HourlyPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeModel", x => x.Id);
                    table.CheckConstraint("CK_BikeModels_HourlyPrice", "[HourlyPrice] > 0");
                    table.CheckConstraint("CK_BikeModels_MaxRiderWeight", "[MaxRiderWeight] > 0");
                    table.CheckConstraint("CK_BikeModels_WheelSize", "[WheelSize] > 0");
                    table.CheckConstraint("CK_BikeModels_Year", "[Year] IS NULL OR ([Year] >= 1900 AND [Year] <= 2100)");
                });

            migrationBuilder.CreateTable(
                name: "Renter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bike",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bike_BikeModel_ModelId",
                        column: x => x.ModelId,
                        principalTable: "BikeModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RentalRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenterId = table.Column<int>(type: "int", nullable: false),
                    BikeId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalRecord", x => x.Id);
                    table.CheckConstraint("CK_RentalRecords_Duration", "[Duration] > '00:00:00'");
                    table.ForeignKey(
                        name: "FK_RentalRecord_Bike_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bike",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RentalRecord_Renter_RenterId",
                        column: x => x.RenterId,
                        principalTable: "Renter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bike_ModelId",
                table: "Bike",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_BikeId",
                table: "RentalRecord",
                column: "BikeId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_RenterId",
                table: "RentalRecord",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RentalRecord_StartTime",
                table: "RentalRecord",
                column: "StartTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentalRecord");

            migrationBuilder.DropTable(
                name: "Bike");

            migrationBuilder.DropTable(
                name: "Renter");

            migrationBuilder.DropTable(
                name: "BikeModel");
        }
    }
}
