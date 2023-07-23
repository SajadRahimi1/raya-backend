using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace raya_back.Migrations
{
    /// <inheritdoc />
    public partial class NewNurseModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "Locations",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Nurses");

            migrationBuilder.RenameColumn(
                name: "About",
                table: "Nurses",
                newName: "PhoneNumber");

            migrationBuilder.AlterColumn<string>(
                name: "PeopleInHouse",
                table: "ReserveNurses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Birthday",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BornCity",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeNumber",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalCode",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalNumber",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SpecialCare",
                table: "Nurses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "NurseFamily",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NurseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KnowTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NurseFamily", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NurseFamily_Nurses_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NurseImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NurseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstPageImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgreementImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NurseImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NurseImages_Nurses_NurseId",
                        column: x => x.NurseId,
                        principalTable: "Nurses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NurseFamily_NurseId",
                table: "NurseFamily",
                column: "NurseId");

            migrationBuilder.CreateIndex(
                name: "IX_NurseImages_NurseId",
                table: "NurseImages",
                column: "NurseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NurseFamily");

            migrationBuilder.DropTable(
                name: "NurseImages");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "BornCity",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "HomeNumber",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "NationalCode",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "NationalNumber",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "SpecialCare",
                table: "Nurses");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Nurses",
                newName: "About");

            migrationBuilder.AlterColumn<string>(
                name: "PeopleInHouse",
                table: "ReserveNurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Days",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Locations",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Nurses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
