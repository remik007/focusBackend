using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class TripAndContactUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hotel",
                table: "Trips",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Meals",
                table: "Trips",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Contacts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Contacts",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hotel",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Meals",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Contacts");
        }
    }
}
