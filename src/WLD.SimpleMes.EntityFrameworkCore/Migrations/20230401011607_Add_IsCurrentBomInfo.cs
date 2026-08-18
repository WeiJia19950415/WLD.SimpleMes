using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations
{
    public partial class Add_IsCurrentBomInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "BomInfos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "BomInfos");
        }
    }
}
