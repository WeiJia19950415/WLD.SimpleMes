using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations
{
    public partial class Update_MaterialStatu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HaveRepaired",
                table: "OrderMaterialProduceStatuses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "NormalWorkProcessId",
                table: "OrderMaterialProduceStatuses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaveRepaired",
                table: "OrderMaterialProduceStatuses");

            migrationBuilder.DropColumn(
                name: "NormalWorkProcessId",
                table: "OrderMaterialProduceStatuses");
        }
    }
}
