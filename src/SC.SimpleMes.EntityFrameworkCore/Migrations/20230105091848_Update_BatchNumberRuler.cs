using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class Update_BatchNumberRuler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialBatchNumberRulers_MaterialInfos_MaterialInfoId",
                table: "MaterialBatchNumberRulers");

            migrationBuilder.DropColumn(
                name: "MaterialNumber",
                table: "MaterialBatchNumberRulers");

            migrationBuilder.RenameColumn(
                name: "MaterialInfoId",
                table: "MaterialBatchNumberRulers",
                newName: "MaterialCategoryInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialBatchNumberRulers_MaterialInfoId",
                table: "MaterialBatchNumberRulers",
                newName: "IX_MaterialBatchNumberRulers_MaterialCategoryInfoId");

            migrationBuilder.AddColumn<long>(
                name: "MaterialBatchNumberRulerId",
                table: "MaterialInfos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ComputePerProductLine",
                table: "MaterialBatchNumberRulers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInfos_MaterialBatchNumberRulerId",
                table: "MaterialInfos",
                column: "MaterialBatchNumberRulerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialBatchNumberRulers_MaterialCategories_MaterialCategoryInfoId",
                table: "MaterialBatchNumberRulers",
                column: "MaterialCategoryInfoId",
                principalTable: "MaterialCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialInfos_MaterialBatchNumberRulers_MaterialBatchNumberRulerId",
                table: "MaterialInfos",
                column: "MaterialBatchNumberRulerId",
                principalTable: "MaterialBatchNumberRulers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaterialBatchNumberRulers_MaterialCategories_MaterialCategoryInfoId",
                table: "MaterialBatchNumberRulers");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialInfos_MaterialBatchNumberRulers_MaterialBatchNumberRulerId",
                table: "MaterialInfos");

            migrationBuilder.DropIndex(
                name: "IX_MaterialInfos_MaterialBatchNumberRulerId",
                table: "MaterialInfos");

            migrationBuilder.DropColumn(
                name: "MaterialBatchNumberRulerId",
                table: "MaterialInfos");

            migrationBuilder.DropColumn(
                name: "ComputePerProductLine",
                table: "MaterialBatchNumberRulers");

            migrationBuilder.RenameColumn(
                name: "MaterialCategoryInfoId",
                table: "MaterialBatchNumberRulers",
                newName: "MaterialInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialBatchNumberRulers_MaterialCategoryInfoId",
                table: "MaterialBatchNumberRulers",
                newName: "IX_MaterialBatchNumberRulers_MaterialInfoId");

            migrationBuilder.AddColumn<string>(
                name: "MaterialNumber",
                table: "MaterialBatchNumberRulers",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialBatchNumberRulers_MaterialInfos_MaterialInfoId",
                table: "MaterialBatchNumberRulers",
                column: "MaterialInfoId",
                principalTable: "MaterialInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
