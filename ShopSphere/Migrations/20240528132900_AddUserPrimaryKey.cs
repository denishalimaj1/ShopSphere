using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopSphere.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRegistrationModel",
                table: "UserRegistrationModel");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "UserRegistrationModel");

            migrationBuilder.RenameTable(
                name: "UserRegistrationModel",
                newName: "UserRegistrationModels");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "UserRegistrationModels",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRegistrationModels",
                table: "UserRegistrationModels",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRegistrationModels",
                table: "UserRegistrationModels");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "UserRegistrationModels");

            migrationBuilder.RenameTable(
                name: "UserRegistrationModels",
                newName: "UserRegistrationModel");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "UserRegistrationModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRegistrationModel",
                table: "UserRegistrationModel",
                column: "Id");
        }
    }
}
