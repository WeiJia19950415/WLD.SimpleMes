using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class MaterialRecord_addMaterialNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InputMaterialNumber",
                table: "WorkProcessMaterialRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InputMaterialNumber",
                table: "WorkProcessMaterialRecordHistory",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputMaterialNumber",
                table: "WorkProcessMaterialRecords");

            migrationBuilder.DropColumn(
                name: "InputMaterialNumber",
                table: "WorkProcessMaterialRecordHistory");
        }
    }
}
