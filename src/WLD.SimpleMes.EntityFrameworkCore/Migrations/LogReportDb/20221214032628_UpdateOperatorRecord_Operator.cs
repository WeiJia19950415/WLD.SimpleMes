using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations.LogReportDb
{
    public partial class UpdateOperatorRecord_Operator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Operator",
                table: "FormInfoRecords",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operator",
                table: "FormInfoRecords");
        }
    }
}
