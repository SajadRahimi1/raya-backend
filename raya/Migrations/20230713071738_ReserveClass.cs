using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class ReserveClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassCategoryUser");

            migrationBuilder.CreateTable(
                name: "ReserveClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsInstallment = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReserveClasses_ClassCategories_ClassCategoryId",
                        column: x => x.ClassCategoryId,
                        principalTable: "ClassCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReserveClasses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReserveClasses_ClassCategoryId",
                table: "ReserveClasses",
                column: "ClassCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveClasses_UserId",
                table: "ReserveClasses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReserveClasses");

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
    }
}
