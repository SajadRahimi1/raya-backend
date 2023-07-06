using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class ReserveNurseUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ReserveNurses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Locations",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveNurses_UserId",
                table: "ReserveNurses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveNurses_Users_UserId",
                table: "ReserveNurses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReserveNurses_Users_UserId",
                table: "ReserveNurses");

            migrationBuilder.DropIndex(
                name: "IX_ReserveNurses_UserId",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ReserveNurses");

            migrationBuilder.AlterColumn<string>(
                name: "Locations",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
