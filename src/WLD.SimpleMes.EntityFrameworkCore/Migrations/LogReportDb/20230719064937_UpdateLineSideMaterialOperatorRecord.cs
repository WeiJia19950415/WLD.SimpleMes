using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class UpdateLineSideMaterialOperatorRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HandleUserId",
                table: "LineSideMaterialOperatorRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "HandleUserName",
                table: "LineSideMaterialOperatorRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandleUserId",
                table: "LineSideMaterialOperatorRecords");

            migrationBuilder.DropColumn(
                name: "HandleUserName",
                table: "LineSideMaterialOperatorRecords");
        }
    }
}
