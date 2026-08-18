using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class AddBOMMaterialCount_MaterialRecordHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InputMaterialUnitName",
                table: "MaterialBatchNumbers",
                newName: "BOMMaterialUnitName");

            migrationBuilder.RenameColumn(
                name: "InputMaterialCount",
                table: "MaterialBatchNumbers",
                newName: "BOMMaterialCount");

            migrationBuilder.AddColumn<decimal>(
                name: "BOMMaterialCount",
                table: "WorkProcessMaterialRecords",
                type: "decimal(20,3)",
                precision: 20,
                scale: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BOMUnitName",
                table: "WorkProcessMaterialRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BOMMaterialCount",
                table: "WorkProcessMaterialRecordHistory",
                type: "decimal(20,3)",
                precision: 20,
                scale: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BOMUnitName",
                table: "WorkProcessMaterialRecordHistory",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BOMMaterialCount",
                table: "WorkProcessMaterialRecords");

            migrationBuilder.DropColumn(
                name: "BOMUnitName",
                table: "WorkProcessMaterialRecords");

            migrationBuilder.DropColumn(
                name: "BOMMaterialCount",
                table: "WorkProcessMaterialRecordHistory");

            migrationBuilder.DropColumn(
                name: "BOMUnitName",
                table: "WorkProcessMaterialRecordHistory");

            migrationBuilder.RenameColumn(
                name: "BOMMaterialUnitName",
                table: "MaterialBatchNumbers",
                newName: "InputMaterialUnitName");

            migrationBuilder.RenameColumn(
                name: "BOMMaterialCount",
                table: "MaterialBatchNumbers",
                newName: "InputMaterialCount");
        }
    }
}
