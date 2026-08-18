using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class InitLogDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbpUserLoginAttempts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    TenancyName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    UserNameOrEmailAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BrowserInfo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Result = table.Column<byte>(type: "tinyint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbpUserLoginAttempts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReturnValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "int", nullable: false),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrowserInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImpersonatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    ImpersonatorTenantId = table.Column<int>(type: "int", nullable: true),
                    CustomData = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormInfoRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BelongOrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BelongMaterialBatchNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BelongWorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    BelongWorkProcessNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BelongFormId = table.Column<long>(type: "bigint", nullable: false),
                    FormRecordData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatorUserId = table.Column<long>(type: "bigint", nullable: false),
                    OperatorTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInfoRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialBatchNumbers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsSerialsNumber = table.Column<bool>(type: "bit", nullable: false),
                    PrintTimes = table.Column<int>(type: "int", nullable: false),
                    MatrialCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromOrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Suppiler = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WrapUniteName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastPrintTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialBatchNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NquartzJobLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeginExcuteTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    JobTypeFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExcpetionMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobResult = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NquartzJobLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProblemDealRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemRecordId = table.Column<long>(type: "bigint", nullable: false),
                    OperatorId = table.Column<long>(type: "bigint", nullable: false),
                    OperatorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DealTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProblemDealType = table.Column<int>(type: "int", nullable: false),
                    OperatorDescreption = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemDealRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProblemRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualityProblemDefineNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BelongProblemDefineId = table.Column<long>(type: "bigint", nullable: false),
                    DetailDescretion = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    BelongWorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    OnWorkProcessNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkOrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BatchMaterilaNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessMaterialRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WrokShopId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    WorkOrderId = table.Column<long>(type: "bigint", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    WorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    WorkProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkStationId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InputMaterilId = table.Column<long>(type: "bigint", nullable: false),
                    InputMaterialBatchNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    InputMaterialCount = table.Column<decimal>(type: "decimal(20,3)", precision: 20, scale: 3, nullable: false),
                    OutRangeCount = table.Column<decimal>(type: "decimal(20,3)", precision: 20, scale: 3, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessMaterialRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessOperatorRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WrokShopId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false),
                    WorkOrderId = table.Column<long>(type: "bigint", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStationId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorkProcessNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BatchNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CostTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    WorkProcessOperateType = table.Column<int>(type: "int", nullable: false),
                    OperatorDescreption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperatroName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpertaorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentOperatroAccountId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessOperatorRecords", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormInfoRecords_BelongFormId",
                table: "FormInfoRecords",
                column: "BelongFormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormInfoRecords_BelongMaterialBatchNumber",
                table: "FormInfoRecords",
                column: "BelongMaterialBatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FormInfoRecords_BelongOrderNumber",
                table: "FormInfoRecords",
                column: "BelongOrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FormInfoRecords_BelongWorkProcessNumber",
                table: "FormInfoRecords",
                column: "BelongWorkProcessNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FormInfoRecords_MaterialId",
                table: "FormInfoRecords",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_FormInfoRecords_MaterialNumber",
                table: "FormInfoRecords",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FormInfoRecords_OperatorTime",
                table: "FormInfoRecords",
                column: "OperatorTime");

            migrationBuilder.CreateIndex(
                name: "IX_FormInfoRecords_OperatorUserId",
                table: "FormInfoRecords",
                column: "OperatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBatchNumbers_BatchNumber",
                table: "MaterialBatchNumbers",
                column: "BatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBatchNumbers_CreationTime",
                table: "MaterialBatchNumbers",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBatchNumbers_MaterialId",
                table: "MaterialBatchNumbers",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBatchNumbers_MaterialNumber",
                table: "MaterialBatchNumbers",
                column: "MaterialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemDealRecords_DealTime",
                table: "ProblemDealRecords",
                column: "DealTime");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemDealRecords_OperatorId",
                table: "ProblemDealRecords",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemDealRecords_ProblemDealType",
                table: "ProblemDealRecords",
                column: "ProblemDealType");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemDealRecords_ProblemRecordId",
                table: "ProblemDealRecords",
                column: "ProblemRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemRecords_BatchMaterilaNumber",
                table: "ProblemRecords",
                column: "BatchMaterilaNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemRecords_CreationTime",
                table: "ProblemRecords",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemRecords_OnWorkProcessNumber",
                table: "ProblemRecords",
                column: "OnWorkProcessNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemRecords_QualityProblemDefineNumber",
                table: "ProblemRecords",
                column: "QualityProblemDefineNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemRecords_WorkOrderNumber",
                table: "ProblemRecords",
                column: "WorkOrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecords_InputMaterialBatchNumber",
                table: "WorkProcessMaterialRecords",
                column: "InputMaterialBatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecords_OrderNumber",
                table: "WorkProcessMaterialRecords",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecords_ProductLineId",
                table: "WorkProcessMaterialRecords",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecords_WorkOrderId",
                table: "WorkProcessMaterialRecords",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecords_WorkProcessId",
                table: "WorkProcessMaterialRecords",
                column: "WorkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessMaterialRecords_WorkStationId",
                table: "WorkProcessMaterialRecords",
                column: "WorkStationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOperatorRecords_BatchNumber",
                table: "WorkProcessOperatorRecords",
                column: "BatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOperatorRecords_CurrentOperatroAccountId",
                table: "WorkProcessOperatorRecords",
                column: "CurrentOperatroAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOperatorRecords_OrderNumber",
                table: "WorkProcessOperatorRecords",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOperatorRecords_ProductLineId",
                table: "WorkProcessOperatorRecords",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOperatorRecords_WorkOrderId",
                table: "WorkProcessOperatorRecords",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOperatorRecords_WorkProcessId",
                table: "WorkProcessOperatorRecords",
                column: "WorkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOperatorRecords_WorkProcessOperateType",
                table: "WorkProcessOperatorRecords",
                column: "WorkProcessOperateType");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessOperatorRecords_WorkStationId",
                table: "WorkProcessOperatorRecords",
                column: "WorkStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbpUserLoginAttempts");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "FormInfoRecords");

            migrationBuilder.DropTable(
                name: "MaterialBatchNumbers");

            migrationBuilder.DropTable(
                name: "NquartzJobLogs");

            migrationBuilder.DropTable(
                name: "ProblemDealRecords");

            migrationBuilder.DropTable(
                name: "ProblemRecords");

            migrationBuilder.DropTable(
                name: "WorkProcessMaterialRecords");

            migrationBuilder.DropTable(
                name: "WorkProcessOperatorRecords");
        }
    }
}
