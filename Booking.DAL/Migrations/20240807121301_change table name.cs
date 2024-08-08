using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changetablename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Country_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestRooms_AspNetUsers_GuestId",
                table: "GuestRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_GuestRooms_Rooms_RoomId",
                table: "GuestRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_GuestRooms_RoomBookingId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuestRooms",
                table: "GuestRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Country",
                table: "Country");

            migrationBuilder.RenameTable(
                name: "GuestRooms",
                newName: "RoomBookings");

            migrationBuilder.RenameTable(
                name: "Country",
                newName: "Countries");

            migrationBuilder.RenameIndex(
                name: "IX_GuestRooms_RoomId",
                table: "RoomBookings",
                newName: "IX_RoomBookings_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_GuestRooms_GuestId",
                table: "RoomBookings",
                newName: "IX_RoomBookings_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomBookings",
                table: "RoomBookings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_RoomBookings_RoomBookingId",
                table: "Reviews",
                column: "RoomBookingId",
                principalTable: "RoomBookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_AspNetUsers_GuestId",
                table: "RoomBookings",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_Rooms_RoomId",
                table: "RoomBookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_RoomBookings_RoomBookingId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookings_AspNetUsers_GuestId",
                table: "RoomBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookings_Rooms_RoomId",
                table: "RoomBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomBookings",
                table: "RoomBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "RoomBookings",
                newName: "GuestRooms");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Country");

            migrationBuilder.RenameIndex(
                name: "IX_RoomBookings_RoomId",
                table: "GuestRooms",
                newName: "IX_GuestRooms_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomBookings_GuestId",
                table: "GuestRooms",
                newName: "IX_GuestRooms_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuestRooms",
                table: "GuestRooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Country",
                table: "Country",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Country_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuestRooms_AspNetUsers_GuestId",
                table: "GuestRooms",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuestRooms_Rooms_RoomId",
                table: "GuestRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_GuestRooms_RoomBookingId",
                table: "Reviews",
                column: "RoomBookingId",
                principalTable: "GuestRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
