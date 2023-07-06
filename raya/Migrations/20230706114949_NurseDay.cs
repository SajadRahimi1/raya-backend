using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class NurseDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "Nurses",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Nurses",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "Nurses");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Nurses",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Nurses",
                newName: "image");

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NurseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    day = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Days_Nurses_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Days_NurseId",
                table: "Days",
                column: "NurseId");
        }
    }
}
