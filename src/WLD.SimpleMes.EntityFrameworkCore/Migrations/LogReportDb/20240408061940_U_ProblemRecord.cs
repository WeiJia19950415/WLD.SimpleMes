using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class U_ProblemRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WokrProcessName",
                table: "ProblemRecords",
                newName: "WorkProcessName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkProcessName",
                table: "ProblemRecords",
                newName: "WokrProcessName");
        }
    }
}
