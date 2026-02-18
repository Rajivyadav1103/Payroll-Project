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
            migrationBuilder.RenameColumn(
                name: "DeductionName",
                table: "Deduction",
                newName: "DeductionHeadName");

            migrationBuilder.RenameColumn(
                name: "DeductionCode",
                table: "Deduction",
                newName: "DeductionHeadCode");

            migrationBuilder.RenameColumn(
                name: "DeductionId",
                table: "Deduction",
                newName: "DeductionHeadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeductionHeadName",
                table: "Deduction",
                newName: "DeductionName");

            migrationBuilder.RenameColumn(
                name: "DeductionHeadCode",
                table: "Deduction",
                newName: "DeductionCode");

            migrationBuilder.RenameColumn(
                name: "DeductionHeadId",
                table: "Deduction",
                newName: "DeductionId");
        }
    }
}
