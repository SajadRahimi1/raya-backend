using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class NurseCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NurseCategory",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NurseCategory",
                table: "Nurses");
        }
    }
}
