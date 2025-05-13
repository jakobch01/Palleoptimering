using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Palleoptimering.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class UpdatedPalletSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxOverhang",
                table: "PalletSettings");

            migrationBuilder.DropColumn(
                name: "SpacingBetweenElements",
                table: "PalletSettings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxOverhang",
                table: "PalletSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpacingBetweenElements",
                table: "PalletSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
