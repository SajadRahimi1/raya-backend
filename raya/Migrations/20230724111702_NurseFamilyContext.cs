using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class NurseFamilyContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NurseFamily_Nurses_NurseId",
                table: "NurseFamily");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NurseFamily",
                table: "NurseFamily");

            migrationBuilder.RenameTable(
                name: "NurseFamily",
                newName: "NurseFamilies");

            migrationBuilder.RenameIndex(
                name: "IX_NurseFamily_NurseId",
                table: "NurseFamilies",
                newName: "IX_NurseFamilies_NurseId");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "NurseFamilies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NurseFamilies",
                table: "NurseFamilies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NurseFamilies_Nurses_NurseId",
                table: "NurseFamilies",
                column: "NurseId",
                principalTable: "Nurses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NurseFamilies_Nurses_NurseId",
                table: "NurseFamilies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NurseFamilies",
                table: "NurseFamilies");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "NurseFamilies");

            migrationBuilder.RenameTable(
                name: "NurseFamilies",
                newName: "NurseFamily");

            migrationBuilder.RenameIndex(
                name: "IX_NurseFamilies_NurseId",
                table: "NurseFamily",
                newName: "IX_NurseFamily_NurseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NurseFamily",
                table: "NurseFamily",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NurseFamily_Nurses_NurseId",
                table: "NurseFamily",
                column: "NurseId",
                principalTable: "Nurses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
