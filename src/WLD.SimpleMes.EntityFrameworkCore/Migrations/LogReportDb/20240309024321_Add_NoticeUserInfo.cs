using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class Add_NoticeUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BelongDepartmentName",
                table: "WarningOverUsedERPInStockInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNoticeUser",
                table: "WarningOverUsedERPInStockInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FirstNoticeUserId",
                table: "WarningOverUsedERPInStockInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BelongDepartmentName",
                table: "WarningOverUsedERPInStockInfos");

            migrationBuilder.DropColumn(
                name: "FirstNoticeUser",
                table: "WarningOverUsedERPInStockInfos");

            migrationBuilder.DropColumn(
                name: "FirstNoticeUserId",
                table: "WarningOverUsedERPInStockInfos");
        }
    }
}
