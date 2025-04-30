using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaPro.Migrations.Internal
{
    /// <inheritdoc />
    public partial class AddFinancialResponsibleAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RgDispatched",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RgDispatchedDate",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CivilState",
                table: "FinancialResponsibles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RgDispatched",
                table: "FinancialResponsibles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RgDispatchedDate",
                table: "FinancialResponsibles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RgDispatched",
                table: "Families",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RgDispatchedDate",
                table: "Families",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RgDispatched",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RgDispatchedDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CivilState",
                table: "FinancialResponsibles");

            migrationBuilder.DropColumn(
                name: "RgDispatched",
                table: "FinancialResponsibles");

            migrationBuilder.DropColumn(
                name: "RgDispatchedDate",
                table: "FinancialResponsibles");

            migrationBuilder.DropColumn(
                name: "RgDispatched",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "RgDispatchedDate",
                table: "Families");
        }
    }
}
