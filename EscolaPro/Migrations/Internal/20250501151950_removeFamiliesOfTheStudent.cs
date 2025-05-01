using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaPro.Migrations.Internal
{
    /// <inheritdoc />
    public partial class removeFamiliesOfTheStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Families_Students_StudentId",
                table: "Families");

            migrationBuilder.DropIndex(
                name: "IX_Families_StudentId",
                table: "Families");

            migrationBuilder.DropColumn(
                name: "ResponsibleEmail",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Families");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsibleEmail",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "Families",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Families_StudentId",
                table: "Families",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Families_Students_StudentId",
                table: "Families",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
