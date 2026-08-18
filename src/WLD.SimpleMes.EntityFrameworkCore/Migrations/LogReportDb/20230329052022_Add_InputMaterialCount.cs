using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class Add_InputMaterialCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "InputMaterialCount",
                table: "MaterialBatchNumbers",
                type: "decimal(20,3)",
                precision: 20,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "InputMaterialUnitName",
                table: "MaterialBatchNumbers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputMaterialCount",
                table: "MaterialBatchNumbers");

            migrationBuilder.DropColumn(
                name: "InputMaterialUnitName",
                table: "MaterialBatchNumbers");
        }
    }
}
