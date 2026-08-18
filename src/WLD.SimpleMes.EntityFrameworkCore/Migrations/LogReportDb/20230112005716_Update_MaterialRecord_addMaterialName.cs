using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class Update_MaterialRecord_addMaterialName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MaterialBatchNumbers_BatchNumber",
                table: "MaterialBatchNumbers");

            migrationBuilder.AddColumn<string>(
                name: "InputMaterialName",
                table: "WorkProcessMaterialRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputUnitName",
                table: "WorkProcessMaterialRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputMaterialName",
                table: "WorkProcessMaterialRecordHistory",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputUnitName",
                table: "WorkProcessMaterialRecordHistory",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBatchNumbers_BatchNumber",
                table: "MaterialBatchNumbers",
                column: "BatchNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MaterialBatchNumbers_BatchNumber",
                table: "MaterialBatchNumbers");

            migrationBuilder.DropColumn(
                name: "InputMaterialName",
                table: "WorkProcessMaterialRecords");

            migrationBuilder.DropColumn(
                name: "InputUnitName",
                table: "WorkProcessMaterialRecords");

            migrationBuilder.DropColumn(
                name: "InputMaterialName",
                table: "WorkProcessMaterialRecordHistory");

            migrationBuilder.DropColumn(
                name: "InputUnitName",
                table: "WorkProcessMaterialRecordHistory");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBatchNumbers_BatchNumber",
                table: "MaterialBatchNumbers",
                column: "BatchNumber");
        }
    }
}
