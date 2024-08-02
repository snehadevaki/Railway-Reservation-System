using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RailwaySystem.API.Migrations
{
    public partial class Ticket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "TransId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "tickets");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PassengerId",
                table: "tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrainId",
                table: "tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "PassengerId",
                table: "tickets");

            migrationBuilder.DropColumn(
                name: "TrainId",
                table: "tickets");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "tickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TransId",
                table: "tickets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
