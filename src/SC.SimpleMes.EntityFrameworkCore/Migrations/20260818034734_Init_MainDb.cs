using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class Init_MainDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtensionData",
                table: "WorkOrderInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentMatrialCount",
                table: "OrderMaterialProduceStatuses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 1m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtensionData",
                table: "WorkOrderInfos");

            migrationBuilder.DropColumn(
                name: "CurrentMatrialCount",
                table: "OrderMaterialProduceStatuses");
        }
    }
}
