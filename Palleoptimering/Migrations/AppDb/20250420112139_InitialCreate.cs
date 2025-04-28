using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Palleoptimering.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Group = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxHeight = table.Column<int>(type: "int", nullable: true),
                    MaxWeight = table.Column<int>(type: "int", nullable: true),
                    Overhang = table.Column<int>(type: "int", nullable: false),
                    AvailableSpaces = table.Column<int>(type: "int", nullable: true),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    SpacingBetweenElements = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PalletSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxLayers = table.Column<int>(type: "int", nullable: false),
                    MaxSpace = table.Column<int>(type: "int", nullable: false),
                    MaxWeightAllowedToRotate = table.Column<int>(type: "int", nullable: false),
                    HeightWidthFactor = table.Column<double>(type: "float", nullable: false),
                    HeightWidthFactorOnlyForSingleElements = table.Column<bool>(type: "bit", nullable: false),
                    MaxStackingHeight = table.Column<int>(type: "int", nullable: false),
                    EndPlateAddition = table.Column<int>(type: "int", nullable: false),
                    MaxAllowedStackingWeight = table.Column<int>(type: "int", nullable: false),
                    AllowRotationWhenExceedingMaxHeight = table.Column<bool>(type: "bit", nullable: false),
                    RowDistribution = table.Column<int>(type: "int", nullable: false),
                    MaxOverhang = table.Column<int>(type: "int", nullable: false),
                    SpacingBetweenElements = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalletSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Elements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Depth = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Mark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Series = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rotation = table.Column<int>(type: "int", nullable: false),
                    RequiresSpecialPallet = table.Column<bool>(type: "bit", nullable: false),
                    MaxElementsPerPallet = table.Column<int>(type: "int", nullable: true),
                    PalletType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsGeometric = table.Column<bool>(type: "bit", nullable: false),
                    OptimizationGroup = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elements_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Elements_OrderId",
                table: "Elements",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Elements");

            migrationBuilder.DropTable(
                name: "Pallets");

            migrationBuilder.DropTable(
                name: "PalletSettings");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
