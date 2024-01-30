using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class imageContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Trips",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "SubPages",
                newName: "ImageName");

            migrationBuilder.AddColumn<string>(
                name: "ImageContent",
                table: "Trips",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageContent",
                table: "SubPages",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContent",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "ImageContent",
                table: "SubPages");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Trips",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "SubPages",
                newName: "ImageUrl");
        }
    }
}
