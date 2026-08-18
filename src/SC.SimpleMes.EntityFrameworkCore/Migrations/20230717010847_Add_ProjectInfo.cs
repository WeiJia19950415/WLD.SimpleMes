using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class Add_ProjectInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "WorkOrderInfos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "LineSideMaterialInfos",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialName",
                table: "LineSideMaterialInfos",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectInfo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProjectNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProjectFullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderInfos_ProjectNumber",
                table: "WorkOrderInfos",
                column: "ProjectNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInfo_ProjectNumber",
                table: "ProjectInfo",
                column: "ProjectNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectInfo");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrderInfos_ProjectNumber",
                table: "WorkOrderInfos");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "WorkOrderInfos");

            migrationBuilder.AlterColumn<string>(
                name: "UnitName",
                table: "LineSideMaterialInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialName",
                table: "LineSideMaterialInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
