using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations.LogReportDb
{
    public partial class AddFlowNumber_MaterialBatchNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsRepaired",
                table: "WorkProcessOperatorRecords",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "FlowNumber",
                table: "MaterialBatchNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBatchNumbers_FlowNumber",
                table: "MaterialBatchNumbers",
                column: "FlowNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MaterialBatchNumbers_FlowNumber",
                table: "MaterialBatchNumbers");

            migrationBuilder.DropColumn(
                name: "FlowNumber",
                table: "MaterialBatchNumbers");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRepaired",
                table: "WorkProcessOperatorRecords",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
