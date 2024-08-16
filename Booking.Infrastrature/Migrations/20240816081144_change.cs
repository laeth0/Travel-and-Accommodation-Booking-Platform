using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastrature.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Residences_ResidenceId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookings_ResidenceBookings_ResidenceBookingUserId_ResidenceBookingResidenceId_ResidenceBookingId",
                table: "RoomBookings");

            migrationBuilder.DropTable(
                name: "ResidenceBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomBookings",
                table: "RoomBookings");

            migrationBuilder.DropIndex(
                name: "IX_RoomBookings_ResidenceBookingUserId_ResidenceBookingResidenceId_ResidenceBookingId",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "ResidenceBookingId",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "ResidenceBookingResidenceId",
                table: "RoomBookings");

            migrationBuilder.RenameColumn(
                name: "ResidenceBookingUserId",
                table: "RoomBookings",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ResidenceId",
                table: "Reviews",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ResidenceId",
                table: "Reviews",
                newName: "IX_Reviews_RoomId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckInDateUtc",
                table: "RoomBookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOutDateUtc",
                table: "RoomBookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "RoomBookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAtUtc",
                table: "RoomBookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPrice",
                table: "RoomBookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserRemarks",
                table: "RoomBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomBookings",
                table: "RoomBookings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_RoomId",
                table: "RoomBookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_UserId",
                table: "RoomBookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Rooms_RoomId",
                table: "Reviews",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_AspNetUsers_UserId",
                table: "RoomBookings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Rooms_RoomId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookings_AspNetUsers_UserId",
                table: "RoomBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomBookings",
                table: "RoomBookings");

            migrationBuilder.DropIndex(
                name: "IX_RoomBookings_RoomId",
                table: "RoomBookings");

            migrationBuilder.DropIndex(
                name: "IX_RoomBookings_UserId",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "CheckInDateUtc",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "CheckOutDateUtc",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "ModifiedAtUtc",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "UserRemarks",
                table: "RoomBookings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RoomBookings",
                newName: "ResidenceBookingUserId");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Reviews",
                newName: "ResidenceId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_RoomId",
                table: "Reviews",
                newName: "IX_Reviews_ResidenceId");

            migrationBuilder.AddColumn<Guid>(
                name: "ResidenceBookingId",
                table: "RoomBookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ResidenceBookingResidenceId",
                table: "RoomBookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomBookings",
                table: "RoomBookings",
                columns: new[] { "RoomId", "ResidenceBookingId", "Id" });

            migrationBuilder.CreateTable(
                name: "ResidenceBookings",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResidenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckInDateUtc = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckOutDateUtc = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuestRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidenceBookings", x => new { x.UserId, x.ResidenceId, x.Id });
                    table.CheckConstraint("CK_CorrectDates", "[CheckOutDateUtc] > [CheckInDateUtc]");
                    table.ForeignKey(
                        name: "FK_ResidenceBookings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResidenceBookings_Residences_ResidenceId",
                        column: x => x.ResidenceId,
                        principalTable: "Residences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_ResidenceBookingUserId_ResidenceBookingResidenceId_ResidenceBookingId",
                table: "RoomBookings",
                columns: new[] { "ResidenceBookingUserId", "ResidenceBookingResidenceId", "ResidenceBookingId" });

            migrationBuilder.CreateIndex(
                name: "IX_ResidenceBookings_ResidenceId",
                table: "ResidenceBookings",
                column: "ResidenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Residences_ResidenceId",
                table: "Reviews",
                column: "ResidenceId",
                principalTable: "Residences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_ResidenceBookings_ResidenceBookingUserId_ResidenceBookingResidenceId_ResidenceBookingId",
                table: "RoomBookings",
                columns: new[] { "ResidenceBookingUserId", "ResidenceBookingResidenceId", "ResidenceBookingId" },
                principalTable: "ResidenceBookings",
                principalColumns: new[] { "UserId", "ResidenceId", "Id" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
