using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastrature.Migrations
{
    /// <inheritdoc />
    public partial class changekeyoftheuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Rooms",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Residences",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Countries",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Cities",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "AspNetUsers",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Residences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePublicId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ImagePublicId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Rooms",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Residences",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Countries",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Cities",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "AspNetUsers",
                newName: "ImageName");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
