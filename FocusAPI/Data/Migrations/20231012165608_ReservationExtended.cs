using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReservationExtended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Reservations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Reservations");
        }
    }
}
