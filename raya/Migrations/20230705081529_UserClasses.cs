using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class UserClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassCategoryUser",
                columns: table => new
                {
                    ReservedClassesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersReservedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCategoryUser", x => new { x.ReservedClassesId, x.UsersReservedId });
                    table.ForeignKey(
                        name: "FK_ClassCategoryUser_ClassCategories_ReservedClassesId",
                        column: x => x.ReservedClassesId,
                        principalTable: "ClassCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassCategoryUser_Users_UsersReservedId",
                        column: x => x.UsersReservedId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassCategoryUser_UsersReservedId",
                table: "ClassCategoryUser",
                column: "UsersReservedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassCategoryUser");
        }
    }
}
