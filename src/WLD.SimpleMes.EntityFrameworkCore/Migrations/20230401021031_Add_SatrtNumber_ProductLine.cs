using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations
{
    public partial class Add_SatrtNumber_ProductLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InitStartNumber",
                table: "ProductLines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitStartNumber",
                table: "ProductLines");
        }
    }
}
