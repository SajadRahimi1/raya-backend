using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class ReserveNurseDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReserveNurses_Nurses_NurseId",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "Days",
                table: "ReserveNurses");

            migrationBuilder.AlterColumn<Guid>(
                name: "NurseId",
                table: "ReserveNurses",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ReserveNurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "ReserveNurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "CCTV",
                table: "ReserveNurses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "ReserveNurses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Hours",
                table: "ReserveNurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NurseCategory",
                table: "ReserveNurses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PeopleInHouse",
                table: "ReserveNurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Shift",
                table: "ReserveNurses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "ReserveNurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveNurses_Nurses_NurseId",
                table: "ReserveNurses",
                column: "NurseId",
                principalTable: "Nurses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReserveNurses_Nurses_NurseId",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "CCTV",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "NurseCategory",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "PeopleInHouse",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "Shift",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "description",
                table: "ReserveNurses");

            migrationBuilder.AlterColumn<Guid>(
                name: "NurseId",
                table: "ReserveNurses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "ReserveNurses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveNurses_Nurses_NurseId",
                table: "ReserveNurses",
                column: "NurseId",
                principalTable: "Nurses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
