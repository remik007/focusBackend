using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class HotelStars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HotelStars",
                table: "Trips",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelStars",
                table: "Trips");
        }
    }
}
