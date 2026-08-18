using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class Add_SaveFormRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BelongProductLineId",
                table: "FormInfoRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "BelongProductLineName",
                table: "FormInfoRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MatreialName",
                table: "FormInfoRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DDImportantInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoulombEfficiency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperatingPressure = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OperatingTemperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VoltageEfficiency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentDensity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnergyEfficiency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ElectrolyteUtilization = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InternalResistanceAfter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InternalResistanceBefore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Scores = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BelongOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MatreialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BelongMaterialBatchNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BelongProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    BelongProductLineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DDImportantInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DDImportantInfos_BelongMaterialBatchNumber",
                table: "DDImportantInfos",
                column: "BelongMaterialBatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DDImportantInfos_BelongProductLineId",
                table: "DDImportantInfos",
                column: "BelongProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_DDImportantInfos_MaterialNumber",
                table: "DDImportantInfos",
                column: "MaterialNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "BelongProductLineId",
                table: "FormInfoRecords");

            migrationBuilder.DropColumn(
                name: "BelongProductLineName",
                table: "FormInfoRecords");

            migrationBuilder.DropColumn(
                name: "MatreialName",
                table: "FormInfoRecords");
        }
    }
}
