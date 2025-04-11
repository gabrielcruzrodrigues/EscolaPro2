using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaPro.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanieInUserGeneral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanieId",
                table: "UsersGeneral",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UsersGeneral_CompanieId",
                table: "UsersGeneral",
                column: "CompanieId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersGeneral_Companies_CompanieId",
                table: "UsersGeneral",
                column: "CompanieId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersGeneral_Companies_CompanieId",
                table: "UsersGeneral");

            migrationBuilder.DropIndex(
                name: "IX_UsersGeneral_CompanieId",
                table: "UsersGeneral");

            migrationBuilder.DropColumn(
                name: "CompanieId",
                table: "UsersGeneral");
        }
    }
}
