using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolls.Migrations
{
    /// <inheritdoc />
    public partial class allowance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Allowances",
                table: "Allowances");

            migrationBuilder.RenameTable(
                name: "Allowances",
                newName: "Allowance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allowance",
                table: "Allowance",
                column: "AllowanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Allowance",
                table: "Allowance");

            migrationBuilder.RenameTable(
                name: "Allowance",
                newName: "Allowances");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Allowances",
                table: "Allowances",
                column: "AllowanceId");
        }
    }
}
