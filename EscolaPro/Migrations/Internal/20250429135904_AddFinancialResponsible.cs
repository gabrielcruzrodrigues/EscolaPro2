using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaPro.Migrations.Internal
{
    /// <inheritdoc />
    public partial class AddFinancialResponsible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Families_ResponsibleId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ResponsibleId",
                table: "Students");

            migrationBuilder.AddColumn<long>(
                name: "FinancialResponsibleId",
                table: "Students",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "FinancialResponsibles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rg = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Naturalness = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Neighborhood = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    State = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialResponsibles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Cpf",
                table: "Students",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_FinancialResponsibleId",
                table: "Students",
                column: "FinancialResponsibleId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Name",
                table: "Students",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Phone",
                table: "Students",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Rg",
                table: "Students",
                column: "Rg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialResponsibles_Cpf",
                table: "FinancialResponsibles",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialResponsibles_Email",
                table: "FinancialResponsibles",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialResponsibles_Name",
                table: "FinancialResponsibles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialResponsibles_Phone",
                table: "FinancialResponsibles",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialResponsibles_Rg",
                table: "FinancialResponsibles",
                column: "Rg",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_FinancialResponsibles_FinancialResponsibleId",
                table: "Students",
                column: "FinancialResponsibleId",
                principalTable: "FinancialResponsibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_FinancialResponsibles_FinancialResponsibleId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "FinancialResponsibles");

            migrationBuilder.DropIndex(
                name: "IX_Students_Cpf",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Email",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_FinancialResponsibleId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Name",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Phone",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_Rg",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FinancialResponsibleId",
                table: "Students");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ResponsibleId",
                table: "Students",
                column: "ResponsibleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Families_ResponsibleId",
                table: "Students",
                column: "ResponsibleId",
                principalTable: "Families",
                principalColumn: "Id");
        }
    }
}
