using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class nurseFormCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "formCode",
                table: "Nurses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "formCode",
                table: "Nurses");
        }
    }
}
