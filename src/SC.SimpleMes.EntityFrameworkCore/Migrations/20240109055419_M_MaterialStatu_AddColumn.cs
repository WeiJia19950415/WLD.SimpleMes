using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class M_MaterialStatu_AddColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailCounts",
                table: "OrderMaterialProduceStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsLastFqcRepaired",
                table: "OrderMaterialProduceStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PassCounts",
                table: "OrderMaterialProduceStatuses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailCounts",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropColumn(
                name: "IsLastFqcRepaired",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropColumn(
                name: "PassCounts",
                table: "OrderMaterialProduceStatuses");
        }
    }
}
