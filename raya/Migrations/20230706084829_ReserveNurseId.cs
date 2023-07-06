using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class ReserveNurseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    day = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NurseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReserveNurseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Days_Nurses_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Days_ReserveNurses_ReserveNurseId",
                        column: x => x.ReserveNurseId,
                        principalTable: "ReserveNurses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Days_NurseId",
                table: "Days",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_Days_ReserveNurseId",
                table: "Days",
                column: "ReserveNurseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Days");
        }
    }
}
