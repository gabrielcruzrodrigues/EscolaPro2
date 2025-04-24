using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EscolaPro.Migrations.Internal
{
    /// <inheritdoc />
    public partial class InitialCreateInternal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FixedHealth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodGroup = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    QuantityBrothers = table.Column<int>(type: "int", nullable: false),
                    ToGoOutAuthorization = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixedHealth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllergieFixedHealth",
                columns: table => new
                {
                    AllergiesId = table.Column<int>(type: "int", nullable: false),
                    FixedHealthsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergieFixedHealth", x => new { x.AllergiesId, x.FixedHealthsId });
                    table.ForeignKey(
                        name: "FK_AllergieFixedHealth_Allergies_AllergiesId",
                        column: x => x.AllergiesId,
                        principalTable: "Allergies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergieFixedHealth_FixedHealth_FixedHealthsId",
                        column: x => x.FixedHealthsId,
                        principalTable: "FixedHealth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ocupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Families", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Families_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergieFixedHealth_FixedHealthsId",
                table: "AllergieFixedHealth",
                column: "FixedHealthsId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_Name",
                table: "Allergies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Families_Cpf",
                table: "Families",
                column: "Cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Families_Email",
                table: "Families",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Families_Name",
                table: "Families",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Families_Phone",
                table: "Families",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Families_Rg",
                table: "Families",
                column: "Rg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Families_StudentId",
                table: "Families",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergieFixedHealth");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "FixedHealth");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
