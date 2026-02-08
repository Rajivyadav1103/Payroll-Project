using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolls.Migrations
{
    /// <inheritdoc />
    public partial class payroleredejhg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserGroups",
                newName: "UserGroupName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserGroups",
                newName: "UserGroupId");

            migrationBuilder.AddColumn<string>(
                name: "IsActive",
                table: "UserGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserGroupCode",
                table: "UserGroups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "createddate",
                table: "UserGroups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "UserGroupCode",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "createddate",
                table: "UserGroups");

            migrationBuilder.RenameColumn(
                name: "UserGroupName",
                table: "UserGroups",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "UserGroupId",
                table: "UserGroups",
                newName: "Id");
        }
    }
}
