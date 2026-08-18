using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class Add_WorkStationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrentProductLineId",
                table: "OrderMaterialProduceStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CurrentWorkStationId",
                table: "OrderMaterialProduceStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentProductLineId",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropColumn(
                name: "CurrentWorkStationId",
                table: "OrderMaterialProduceStatuses");
        }
    }
}
