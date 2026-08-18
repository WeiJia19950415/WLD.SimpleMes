using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class Update_ProblemDailyReportTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProblemCount",
                table: "WorkProcessProblemDailyReportRecords",
                newName: "DataCount");

            migrationBuilder.AlterColumn<string>(
                name: "WorkStationName",
                table: "WorkProcessProblemDailyReportRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WorkProcessName",
                table: "WorkProcessProblemDailyReportRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductLineName",
                table: "WorkProcessProblemDailyReportRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "ProbleName",
                table: "WorkProcessProblemDailyReportRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ProblemDefineId",
                table: "WorkProcessProblemDailyReportRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "QualityProblemNumber",
                table: "WorkProcessProblemDailyReportRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_ProblemDefineId",
                table: "WorkProcessProblemDailyReportRecords",
                column: "ProblemDefineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_QualityProblemNumber",
                table: "WorkProcessProblemDailyReportRecords",
                column: "QualityProblemNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_ProblemDefineId",
                table: "WorkProcessProblemDailyReportRecords");

            migrationBuilder.DropIndex(
                name: "IX_WorkProcessProblemDailyReportRecords_QualityProblemNumber",
                table: "WorkProcessProblemDailyReportRecords");

            migrationBuilder.DropColumn(
                name: "ProbleName",
                table: "WorkProcessProblemDailyReportRecords");

            migrationBuilder.DropColumn(
                name: "ProblemDefineId",
                table: "WorkProcessProblemDailyReportRecords");

            migrationBuilder.DropColumn(
                name: "QualityProblemNumber",
                table: "WorkProcessProblemDailyReportRecords");

            migrationBuilder.RenameColumn(
                name: "DataCount",
                table: "WorkProcessProblemDailyReportRecords",
                newName: "ProblemCount");

            migrationBuilder.AlterColumn<string>(
                name: "WorkStationName",
                table: "WorkProcessProblemDailyReportRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "WorkProcessName",
                table: "WorkProcessProblemDailyReportRecords",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "ProductLineName",
                table: "WorkProcessProblemDailyReportRecords",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);
        }
    }
}
