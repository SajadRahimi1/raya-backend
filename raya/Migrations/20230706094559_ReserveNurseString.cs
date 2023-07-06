using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class ReserveNurseString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Days_ReserveNurses_ReserveNurseId",
                table: "Days");

            migrationBuilder.DropIndex(
                name: "IX_Days_ReserveNurseId",
                table: "Days");

            migrationBuilder.DropColumn(
                name: "ReserveNurseId",
                table: "Days");

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "ReserveNurses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "ReserveNurses");

            migrationBuilder.AddColumn<Guid>(
                name: "ReserveNurseId",
                table: "Days",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Days_ReserveNurseId",
                table: "Days",
                column: "ReserveNurseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Days_ReserveNurses_ReserveNurseId",
                table: "Days",
                column: "ReserveNurseId",
                principalTable: "ReserveNurses",
                principalColumn: "Id");
        }
    }
}
