using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SC.SimpleMes.Migrations
{
    public partial class CutMatierl_AddProductMaterilaNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CutMaterialConfig_MaterialInfos_UsedProductId",
                table: "CutMaterialConfig");

            migrationBuilder.DropColumn(
                name: "CanMixUsed",
                table: "MaterialReplaceRelations");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "MaterialReplaceRelations");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "MaterialReplaceRelations");

            migrationBuilder.DropColumn(
                name: "MaterialReplaceType",
                table: "MaterialReplaceRelations");

            migrationBuilder.RenameColumn(
                name: "SourceMaterId",
                table: "MaterialReplaceRelations",
                newName: "FsubsItemID");

            migrationBuilder.RenameColumn(
                name: "ReplaceMaterilId",
                table: "MaterialReplaceRelations",
                newName: "Fapplicableltem");

            migrationBuilder.RenameColumn(
                name: "IsEnable",
                table: "MaterialReplaceRelations",
                newName: "Fstatus");

            migrationBuilder.AddColumn<string>(
                name: "FApplicableBOM",
                table: "MaterialReplaceRelations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FItemID",
                table: "MaterialReplaceRelations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "FItenCode",
                table: "MaterialReplaceRelations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FapplicableItenCode",
                table: "MaterialReplaceRelations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Frate",
                table: "MaterialReplaceRelations",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "FsubsItenlode",
                table: "MaterialReplaceRelations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "UsedProductId",
                table: "CutMaterialConfig",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "ConfigMaterialNumber",
                table: "CutMaterialConfig",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductMaterialNumber",
                table: "CutMaterialConfig",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CutMaterialConfig_MaterialInfos_UsedProductId",
                table: "CutMaterialConfig",
                column: "UsedProductId",
                principalTable: "MaterialInfos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CutMaterialConfig_MaterialInfos_UsedProductId",
                table: "CutMaterialConfig");

            migrationBuilder.DropColumn(
                name: "FApplicableBOM",
                table: "MaterialReplaceRelations");

            migrationBuilder.DropColumn(
                name: "FItemID",
                table: "MaterialReplaceRelations");

            migrationBuilder.DropColumn(
                name: "FItenCode",
                table: "MaterialReplaceRelations");

            migrationBuilder.DropColumn(
                name: "FapplicableItenCode",
                table: "MaterialReplaceRelations");

            migrationBuilder.DropColumn(
                name: "Frate",
                table: "MaterialReplaceRelations");

            migrationBuilder.DropColumn(
                name: "FsubsItenlode",
                table: "MaterialReplaceRelations");

            migrationBuilder.DropColumn(
                name: "ProductMaterialNumber",
                table: "CutMaterialConfig");

            migrationBuilder.RenameColumn(
                name: "FsubsItemID",
                table: "MaterialReplaceRelations",
                newName: "SourceMaterId");

            migrationBuilder.RenameColumn(
                name: "Fstatus",
                table: "MaterialReplaceRelations",
                newName: "IsEnable");

            migrationBuilder.RenameColumn(
                name: "Fapplicableltem",
                table: "MaterialReplaceRelations",
                newName: "ReplaceMaterilId");

            migrationBuilder.AddColumn<bool>(
                name: "CanMixUsed",
                table: "MaterialReplaceRelations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "MaterialReplaceRelations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "MaterialReplaceRelations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialReplaceType",
                table: "MaterialReplaceRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "UsedProductId",
                table: "CutMaterialConfig",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConfigMaterialNumber",
                table: "CutMaterialConfig",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CutMaterialConfig_MaterialInfos_UsedProductId",
                table: "CutMaterialConfig",
                column: "UsedProductId",
                principalTable: "MaterialInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
