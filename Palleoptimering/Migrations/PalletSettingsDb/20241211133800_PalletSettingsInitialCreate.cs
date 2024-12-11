using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Palleoptimering.Migrations.PalletSettingsDb
{
    /// <inheritdoc />
    public partial class PalletSettingsInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PalletSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxLayers = table.Column<int>(type: "int", nullable: false),
                    MaxSpace = table.Column<int>(type: "int", nullable: false),
                    WeightAllowedToTurnElement = table.Column<int>(type: "int", nullable: false),
                    HeightWidthFactor = table.Column<double>(type: "float", nullable: false),
                    HeightWidthFactorOnlyForOneElement = table.Column<bool>(type: "bit", nullable: false),
                    StackingMaxHeight = table.Column<double>(type: "float", nullable: false),
                    EndPlate = table.Column<int>(type: "int", nullable: false),
                    AllowedStackingMaxWeight = table.Column<int>(type: "int", nullable: false),
                    AllowTurningOverMaxHeight = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalletSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PalletSettings");
        }
    }
}
