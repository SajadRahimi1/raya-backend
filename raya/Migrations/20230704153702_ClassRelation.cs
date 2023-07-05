using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class ClassRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassCategories_Classes_Id",
                table: "ClassCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ReserveNurses_Nurses_Id",
                table: "ReserveNurses");

            migrationBuilder.AddColumn<Guid>(
                name: "NurseId",
                table: "ReserveNurses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ClassId",
                table: "ClassCategories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ReserveNurses_NurseId",
                table: "ReserveNurses",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassCategories_ClassId",
                table: "ClassCategories",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassCategories_Classes_ClassId",
                table: "ClassCategories",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveNurses_Nurses_NurseId",
                table: "ReserveNurses",
                column: "NurseId",
                principalTable: "Nurses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassCategories_Classes_ClassId",
                table: "ClassCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_ReserveNurses_Nurses_NurseId",
                table: "ReserveNurses");

            migrationBuilder.DropIndex(
                name: "IX_ReserveNurses_NurseId",
                table: "ReserveNurses");

            migrationBuilder.DropIndex(
                name: "IX_ClassCategories_ClassId",
                table: "ClassCategories");

            migrationBuilder.DropColumn(
                name: "NurseId",
                table: "ReserveNurses");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "ClassCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassCategories_Classes_Id",
                table: "ClassCategories",
                column: "Id",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReserveNurses_Nurses_Id",
                table: "ReserveNurses",
                column: "Id",
                principalTable: "Nurses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
