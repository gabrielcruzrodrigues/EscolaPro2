using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaPro.Migrations.Internal
{
    /// <inheritdoc />
    public partial class AddFixedHealth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllergieFixedHealth_FixedHealth_FixedHealthsId",
                table: "AllergieFixedHealth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllergieFixedHealth",
                table: "AllergieFixedHealth");

            migrationBuilder.DropIndex(
                name: "IX_AllergieFixedHealth_FixedHealthsId",
                table: "AllergieFixedHealth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FixedHealth",
                table: "FixedHealth");

            migrationBuilder.DropColumn(
                name: "FixedHealthsId",
                table: "AllergieFixedHealth");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FixedHealth");

            migrationBuilder.RenameTable(
                name: "FixedHealth",
                newName: "FixedHealths");

            migrationBuilder.AddColumn<long>(
                name: "FixedHealthsStudentId",
                table: "AllergieFixedHealth",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StudentId",
                table: "FixedHealths",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllergieFixedHealth",
                table: "AllergieFixedHealth",
                columns: new[] { "AllergiesId", "FixedHealthsStudentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FixedHealths",
                table: "FixedHealths",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AllergieFixedHealth_FixedHealthsStudentId",
                table: "AllergieFixedHealth",
                column: "FixedHealthsStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllergieFixedHealth_FixedHealths_FixedHealthsStudentId",
                table: "AllergieFixedHealth",
                column: "FixedHealthsStudentId",
                principalTable: "FixedHealths",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FixedHealths_Students_StudentId",
                table: "FixedHealths",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllergieFixedHealth_FixedHealths_FixedHealthsStudentId",
                table: "AllergieFixedHealth");

            migrationBuilder.DropForeignKey(
                name: "FK_FixedHealths_Students_StudentId",
                table: "FixedHealths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllergieFixedHealth",
                table: "AllergieFixedHealth");

            migrationBuilder.DropIndex(
                name: "IX_AllergieFixedHealth_FixedHealthsStudentId",
                table: "AllergieFixedHealth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FixedHealths",
                table: "FixedHealths");

            migrationBuilder.DropColumn(
                name: "FixedHealthsStudentId",
                table: "AllergieFixedHealth");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "FixedHealths");

            migrationBuilder.RenameTable(
                name: "FixedHealths",
                newName: "FixedHealth");

            migrationBuilder.AddColumn<int>(
                name: "FixedHealthsId",
                table: "AllergieFixedHealth",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FixedHealth",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllergieFixedHealth",
                table: "AllergieFixedHealth",
                columns: new[] { "AllergiesId", "FixedHealthsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FixedHealth",
                table: "FixedHealth",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AllergieFixedHealth_FixedHealthsId",
                table: "AllergieFixedHealth",
                column: "FixedHealthsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AllergieFixedHealth_FixedHealth_FixedHealthsId",
                table: "AllergieFixedHealth",
                column: "FixedHealthsId",
                principalTable: "FixedHealth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
