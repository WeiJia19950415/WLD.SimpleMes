using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class M_ProblemRecord_DeptName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "ProblemRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProblemRecords_ReoonsibleDepartmentId",
                table: "ProblemRecords",
                column: "ReoonsibleDepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProblemRecords_ReoonsibleDepartmentId",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "ProblemRecords");
        }
    }
}
