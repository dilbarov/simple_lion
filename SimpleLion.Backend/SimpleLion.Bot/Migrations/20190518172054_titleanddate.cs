using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleLion.Bot.Migrations
{
    public partial class titleanddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "CommandStates",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "CommandStates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "CommandStates");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "CommandStates");
        }
    }
}
