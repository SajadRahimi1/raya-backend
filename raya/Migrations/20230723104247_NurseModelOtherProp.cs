using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class NurseModelOtherProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherProp",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherProp",
                table: "Nurses");
        }
    }
}
