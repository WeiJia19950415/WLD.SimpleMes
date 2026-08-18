using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class UpdateTableAddWorkProcessname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShared",
                table: "WorkStationInfos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "FinishedCount",
                table: "WorkOrderInfos",
                type: "decimal(20,3)",
                precision: 20,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProdcuingCount",
                table: "WorkOrderInfos",
                type: "decimal(20,3)",
                precision: 20,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "BomItemInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShared",
                table: "WorkStationInfos");

            migrationBuilder.DropColumn(
                name: "FinishedCount",
                table: "WorkOrderInfos");

            migrationBuilder.DropColumn(
                name: "ProdcuingCount",
                table: "WorkOrderInfos");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "BomItemInfos");
        }
    }
}
