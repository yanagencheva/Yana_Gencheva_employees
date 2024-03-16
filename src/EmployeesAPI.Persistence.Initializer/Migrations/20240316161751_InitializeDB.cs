using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeesAPI.Persistence.Initializer.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpID = table.Column<long>(type: "int8", nullable: false),
                    ProjectID = table.Column<long>(type: "int8", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjects", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProjects");
        }
    }
}
