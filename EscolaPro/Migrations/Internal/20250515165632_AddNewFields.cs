using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaPro.Migrations.Internal
{
    /// <inheritdoc />
    public partial class AddNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkAddress",
                table: "Families",
                newName: "RgFilePath");

            migrationBuilder.RenameColumn(
                name: "Ocupation",
                table: "Families",
                newName: "ProofOfResidenceFilePath");

            migrationBuilder.AddColumn<string>(
                name: "CpfFilePath",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeNumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProofOfResidenceFilePath",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RgFilePath",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpfFilePath",
                table: "FinancialResponsibles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeNumber",
                table: "FinancialResponsibles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProofOfResidenceFilePath",
                table: "FinancialResponsibles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RgFilePath",
                table: "FinancialResponsibles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpfFilePath",
                table: "Families",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeNumber",
                table: "Families",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpfFilePath",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HomeNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ProofOfResidenceFilePath",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RgFilePath",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CpfFilePath",
                table: "FinancialResponsibles");

            migrationBuilder.DropColumn(
                name: "HomeNumber",
                table: "FinancialResponsibles");

            migrationBuilder.DropColumn(
                name: "ProofOfResidenceFilePath",
                table: "FinancialResponsibles");

            migrationBuilder.DropColumn(
                name: "RgFilePath",
                table: "FinancialResponsibles");

            migrationBuilder.DropColumn(
                name: "CpfFilePath",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "HomeNumber",
                table: "Families");

            migrationBuilder.RenameColumn(
                name: "RgFilePath",
                table: "Families",
                newName: "WorkAddress");

            migrationBuilder.RenameColumn(
                name: "ProofOfResidenceFilePath",
                table: "Families",
                newName: "Ocupation");
        }
    }
}
