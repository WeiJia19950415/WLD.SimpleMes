using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class Add_DiscardInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReoonsibleDepartmentId",
                table: "ProblemRecords",
                newName: "ResponsibleDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProblemRecords_ReoonsibleDepartmentId",
                table: "ProblemRecords",
                newName: "IX_ProblemRecords_ResponsibleDepartmentId");

            migrationBuilder.AlterColumn<string>(
                name: "WorkOrderNumber",
                table: "ProblemRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<decimal>(
                name: "CheckCount",
                table: "ProblemRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 1m);

            migrationBuilder.AddColumn<string>(
                name: "MaterialNumber",
                table: "ProblemRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatreialName",
                table: "ProblemRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProblemCount",
                table: "ProblemRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 1m);

            migrationBuilder.AddColumn<string>(
                name: "ReasonAnlysis",
                table: "ProblemRecords",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitName",
                table: "ProblemRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MaterialDiscardRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemRecordId = table.Column<long>(type: "bigint", nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ErpBatchNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiccardCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RecordUserId = table.Column<long>(type: "bigint", maxLength: 255, nullable: false),
                    RecordUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscardType = table.Column<int>(type: "int", nullable: false),
                    DeiscardReasonDescreption = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ProblemDefineId = table.Column<long>(type: "bigint", nullable: true),
                    ProblemDefineNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDiscardRecord", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProblemRecords_MaterialNumber",
                table: "ProblemRecords",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDiscardRecord_MaterialNumber",
                table: "MaterialDiscardRecord",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDiscardRecord_ProblemRecordId",
                table: "MaterialDiscardRecord",
                column: "ProblemRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDiscardRecord_RecordDate",
                table: "MaterialDiscardRecord",
                column: "RecordDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialDiscardRecord");

            migrationBuilder.DropIndex(
                name: "IX_ProblemRecords_MaterialNumber",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "CheckCount",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "MaterialNumber",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "MatreialName",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "ProblemCount",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "ReasonAnlysis",
                table: "ProblemRecords");

            migrationBuilder.DropColumn(
                name: "UnitName",
                table: "ProblemRecords");

            migrationBuilder.RenameColumn(
                name: "ResponsibleDepartmentId",
                table: "ProblemRecords",
                newName: "ReoonsibleDepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProblemRecords_ResponsibleDepartmentId",
                table: "ProblemRecords",
                newName: "IX_ProblemRecords_ReoonsibleDepartmentId");

            migrationBuilder.AlterColumn<string>(
                name: "WorkOrderNumber",
                table: "ProblemRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
