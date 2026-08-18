using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class Modify_DDImportInfo_AddProjectInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "DDImportantInfos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectNumber",
                table: "DDImportantInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "DDImportantInfos");

            migrationBuilder.DropColumn(
                name: "ProjectNumber",
                table: "DDImportantInfos");
        }
    }
}
