using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class WorkProcessOperLog_AddIsRepaired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRepaired",
                table: "WorkProcessOperatorRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ResponsibleWorkProcessId",
                table: "ProblemRecords",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRepaired",
                table: "WorkProcessOperatorRecords");

            migrationBuilder.DropColumn(
                name: "ResponsibleWorkProcessId",
                table: "ProblemRecords");
        }
    }
}
