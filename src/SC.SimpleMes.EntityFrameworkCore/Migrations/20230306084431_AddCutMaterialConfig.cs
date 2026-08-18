using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class AddCutMaterialConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CutMaterialConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsedProductId = table.Column<long>(type: "bigint", nullable: false),
                    ConfigMaterialNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ConfigMaterialName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ConfigMaterialUnitName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CutSpecification = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CutUnitName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ConversionRatio = table.Column<decimal>(type: "decimal(15,4)", precision: 15, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CutMaterialConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CutMaterialConfig_MaterialInfos_UsedProductId",
                        column: x => x.UsedProductId,
                        principalTable: "MaterialInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CutMaterialConfig_ConfigMaterialNumber",
                table: "CutMaterialConfig",
                column: "ConfigMaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CutMaterialConfig_UsedProductId",
                table: "CutMaterialConfig",
                column: "UsedProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CutMaterialConfig");
        }
    }
}
