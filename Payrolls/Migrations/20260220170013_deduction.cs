using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolls.Migrations
{
    /// <inheritdoc />
    public partial class deduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAllowanceInfos_Employee_EmployeeId",
                table: "EmployeeAllowanceInfos");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "EmployeeAllowanceInfos");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "EmployeeAllowanceInfos",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "EmployeeAllowanceInfos",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeAllowanceInfos_EmployeeId",
                table: "EmployeeAllowanceInfos",
                newName: "IX_EmployeeAllowanceInfos_EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAllowanceInfos_Employee_EmployeeID",
                table: "EmployeeAllowanceInfos",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAllowanceInfos_Employee_EmployeeID",
                table: "EmployeeAllowanceInfos");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "EmployeeAllowanceInfos",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "EmployeeAllowanceInfos",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeAllowanceInfos_EmployeeID",
                table: "EmployeeAllowanceInfos",
                newName: "IX_EmployeeAllowanceInfos_EmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeID",
                table: "EmployeeAllowanceInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAllowanceInfos_Employee_EmployeeId",
                table: "EmployeeAllowanceInfos",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
