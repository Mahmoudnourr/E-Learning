using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                table: "RefreshToken",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "DomainUserId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_TokenHash",
                table: "RefreshToken",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DomainUserId",
                table: "AspNetUsers",
                column: "DomainUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Users_DomainUserId",
                table: "AspNetUsers",
                column: "DomainUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Users_DomainUserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_TokenHash",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DomainUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DomainUserId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                table: "RefreshToken",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
