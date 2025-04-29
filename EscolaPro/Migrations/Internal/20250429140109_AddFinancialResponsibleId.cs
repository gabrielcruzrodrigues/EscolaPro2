using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaPro.Migrations.Internal
{
    /// <inheritdoc />
    public partial class AddFinancialResponsibleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_FinancialResponsibles_FinancialResponsibleId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ResponsibleId",
                table: "Students");

            migrationBuilder.AlterColumn<long>(
                name: "FinancialResponsibleId",
                table: "Students",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_FinancialResponsibles_FinancialResponsibleId",
                table: "Students",
                column: "FinancialResponsibleId",
                principalTable: "FinancialResponsibles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_FinancialResponsibles_FinancialResponsibleId",
                table: "Students");

            migrationBuilder.AlterColumn<long>(
                name: "FinancialResponsibleId",
                table: "Students",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ResponsibleId",
                table: "Students",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_FinancialResponsibles_FinancialResponsibleId",
                table: "Students",
                column: "FinancialResponsibleId",
                principalTable: "FinancialResponsibles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
