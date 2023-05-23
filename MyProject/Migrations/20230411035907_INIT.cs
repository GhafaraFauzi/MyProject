using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProject.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Departemen",
                columns: table => new
                {
                    IdDepartemen = table.Column<int>(name: "Id_Departemen", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaDepartemen = table.Column<string>(name: "Nama_Departemen", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Departemen", x => x.IdDepartemen);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Employee",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DepartemenIdDepartemen = table.Column<int>(name: "DepartemenId_Departemen", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Employee", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Tbl_Employee_Tbl_Departemen_DepartemenId_Departemen",
                        column: x => x.DepartemenIdDepartemen,
                        principalTable: "Tbl_Departemen",
                        principalColumn: "Id_Departemen");
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Account",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Account", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_Tbl_Account_Tbl_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "Tbl_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Employee_DepartemenId_Departemen",
                table: "Tbl_Employee",
                column: "DepartemenId_Departemen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Account");

            migrationBuilder.DropTable(
                name: "Tbl_Employee");

            migrationBuilder.DropTable(
                name: "Tbl_Departemen");
        }
    }
}
