using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace StapeleyDigital.AthleticsData.Data.Migrations
{
    public partial class AgeGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Standards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Standards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AgeGroup",
                table: "EventStandards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgeGroup",
                table: "Athletes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Standards");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Standards");

            migrationBuilder.DropColumn(
                name: "AgeGroup",
                table: "EventStandards");

            migrationBuilder.DropColumn(
                name: "AgeGroup",
                table: "Athletes");
        }
    }
}
