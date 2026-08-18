using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class Update_BatchNumberInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreateProductLineId",
                table: "MaterialBatchNumbers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreateWorkStationId",
                table: "MaterialBatchNumbers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateWorkStationName",
                table: "MaterialBatchNumbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creator",
                table: "MaterialBatchNumbers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateProductLineId",
                table: "MaterialBatchNumbers");

            migrationBuilder.DropColumn(
                name: "CreateWorkStationId",
                table: "MaterialBatchNumbers");

            migrationBuilder.DropColumn(
                name: "CreateWorkStationName",
                table: "MaterialBatchNumbers");

            migrationBuilder.DropColumn(
                name: "Creator",
                table: "MaterialBatchNumbers");
        }
    }
}
