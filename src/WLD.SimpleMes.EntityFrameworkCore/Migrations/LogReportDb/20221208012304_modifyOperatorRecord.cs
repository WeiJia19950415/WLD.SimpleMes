using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class modifyOperatorRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "WorkOrderId",
                table: "WorkProcessOperatorRecords",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "WorkOrderId",
                table: "WorkProcessMaterialRecords",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "InputMaterialCount",
                table: "WorkProcessMaterialRecords",
                type: "decimal(20,3)",
                precision: 20,
                scale: 3,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)",
                oldPrecision: 20,
                oldScale: 3);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "WorkOrderId",
                table: "WorkProcessOperatorRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "WorkOrderId",
                table: "WorkProcessMaterialRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InputMaterialCount",
                table: "WorkProcessMaterialRecords",
                type: "decimal(20,3)",
                precision: 20,
                scale: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,3)",
                oldPrecision: 20,
                oldScale: 3,
                oldNullable: true);
        }
    }
}
