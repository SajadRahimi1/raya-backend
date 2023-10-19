using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class NurseStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "code",
                table: "Admins",
                newName: "smsCode");

            migrationBuilder.AddColumn<string>(
                name: "Shift",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Nurses",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Shift",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Nurses");

            migrationBuilder.RenameColumn(
                name: "smsCode",
                table: "Admins",
                newName: "code");
        }
    }
}
