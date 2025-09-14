using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvancApi.Migrations
{
    /// <inheritdoc />
    public partial class RenameSalaryToMaxFixedAmount_AddCompanyAndDocumentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Employees",
                newName: "MaxFixedAmount");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentId",
                table: "Employees",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "MaxFixedAmount",
                table: "Employees",
                newName: "Salary");
        }
    }
}
