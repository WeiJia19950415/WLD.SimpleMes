using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLD.SimpleMes.Migrations
{
    public partial class InitSimpleMesDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Parameters = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ReturnValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExecutionDuration = table.Column<int>(type: "int", nullable: false),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    BrowserInfo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ExceptionMessage = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ImpersonatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    ImpersonatorTenantId = table.Column<int>(type: "int", nullable: true),
                    CustomData = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BackgroundJobs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobType = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    JobArgs = table.Column<string>(type: "nvarchar(max)", maxLength: 1048576, nullable: false),
                    TryCount = table.Column<short>(type: "smallint", nullable: false),
                    NextTryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastTryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAbandoned = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<byte>(type: "tinyint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InputType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Editions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityChangeSets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrowserInfo = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ClientIpAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ClientName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExtensionData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImpersonatorTenantId = table.Column<int>(type: "int", nullable: true),
                    ImpersonatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityChangeSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ERPSyncTasks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SyncType = table.Column<int>(type: "int", nullable: false),
                    StoredName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StoredParameter = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SyncState = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImplementTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailMassage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERPSyncTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormTemplateInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    FormsName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TemplateData = table.Column<string>(type: "text", nullable: true),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplateInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageTexts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    LanguageName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Source = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", maxLength: 67108864, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CategoryDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsKeyMaterial = table.Column<bool>(type: "bit", nullable: false),
                    ParentCategoryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialReplaceRelations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceMaterId = table.Column<long>(type: "bigint", nullable: false),
                    ReplaceMaterilId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialReplaceType = table.Column<int>(type: "int", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    CanMixUsed = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialReplaceRelations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotificationName = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 1048576, nullable: true),
                    DataTypeName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EntityTypeAssemblyQualifiedName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    Severity = table.Column<byte>(type: "tinyint", nullable: false),
                    UserIds = table.Column<string>(type: "nvarchar(max)", maxLength: 131072, nullable: true),
                    ExcludedUserIds = table.Column<string>(type: "nvarchar(max)", maxLength: 131072, nullable: true),
                    TenantIds = table.Column<string>(type: "nvarchar(max)", maxLength: 131072, nullable: true),
                    TargetNotifiers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    NotificationName = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    EntityTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EntityTypeAssemblyQualifiedName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUnitRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    OrganizationUnitId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUnitRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortNumber = table.Column<long>(type: "bigint", nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationUnits_OrganizationUnits_ParentId",
                        column: x => x.ParentId,
                        principalTable: "OrganizationUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProblemCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullCategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ParentCategoryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    NotificationName = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 1048576, nullable: true),
                    DataTypeName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    EntityTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EntityTypeAssemblyQualifiedName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    EntityId = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    Severity = table.Column<byte>(type: "tinyint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    UserLinkId = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginAttempts",
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
                    table.PrimaryKey("PK_UserLoginAttempts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    TenantNotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TargetNotifiers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationUnitId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrganizationUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCard = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SortNumber = table.Column<int>(type: "int", nullable: false),
                    WorkNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeChatOpenId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthenticationSource = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EmailConfirmationCode = table.Column<string>(type: "nvarchar(328)", maxLength: 328, nullable: true),
                    PasswordResetCode = table.Column<string>(type: "nvarchar(328)", maxLength: 328, nullable: true),
                    LockoutEndDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    IsLockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    IsPhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsTwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmailAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Users_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Users_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WebhookEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebhookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebhookSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    WebhookUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Webhooks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Headers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookSubscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    ProcessName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProcessNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CanJump = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    WorkProcessPowerType = table.Column<int>(type: "int", nullable: true),
                    WorkProcessType = table.Column<int>(type: "int", nullable: false),
                    StandWorkTime = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExtensionData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessSets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SetVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Descreption = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GraphData = table.Column<string>(type: "text", nullable: true),
                    ExtensionData = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessSets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkShopInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    WorkShopName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkShopNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShopInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicEntityProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityFullName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DynamicPropertyId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicEntityProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicEntityProperties_DynamicProperties_DynamicPropertyId",
                        column: x => x.DynamicPropertyId,
                        principalTable: "DynamicProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DynamicPropertyValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    DynamicPropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicPropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicPropertyValues_DynamicProperties_DynamicPropertyId",
                        column: x => x.DynamicPropertyId,
                        principalTable: "DynamicProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EditionId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_Editions_EditionId",
                        column: x => x.EditionId,
                        principalTable: "Editions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityChanges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeType = table.Column<byte>(type: "tinyint", nullable: false),
                    EntityChangeSetId = table.Column<long>(type: "bigint", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(48)", maxLength: 48, nullable: true),
                    EntityTypeFullName = table.Column<string>(type: "nvarchar(192)", maxLength: 192, nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityChanges_EntityChangeSets_EntityChangeSetId",
                        column: x => x.EntityChangeSetId,
                        principalTable: "EntityChangeSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                       ,
                    MaterialName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialCategoryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    BelongCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    MaterialType = table.Column<int>(type: "int", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Specification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialInfos_MaterialCategories_BelongCategoryId",
                        column: x => x.BelongCategoryId,
                        principalTable: "MaterialCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QualityProblemDefines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QualityProblemNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProbleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ProblemCategoryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityProblemDefines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualityProblemDefines_ProblemCategories_ProblemCategoryId",
                        column: x => x.ProblemCategoryId,
                        principalTable: "ProblemCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    IsStatic = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Users_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Roles_Users_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Roles_Users_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniformSocialCreditCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LogoImage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AreaCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OfficialWebsite = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsAuthentication = table.Column<bool>(type: "bit", nullable: false),
                    BriefIntroduction = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    TenantScale = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenancyName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EditionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tenants_Editions_EditionId",
                        column: x => x.EditionId,
                        principalTable: "Editions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tenants_Users_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tenants_Users_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tenants_Users_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebhookSendAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebhookEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebhookSubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseStatusCode = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookSendAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebhookSendAttempts_WebhookEvents_WebhookEventId",
                        column: x => x.WebhookEventId,
                        principalTable: "WebhookEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessFormInfoRelations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BelongWorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    BelongFormInfoId = table.Column<long>(type: "bigint", nullable: false),
                    FormUseType = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessFormInfoRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkProcessFormInfoRelations_FormTemplateInfos_BelongFormInfoId",
                        column: x => x.BelongFormInfoId,
                        principalTable: "FormTemplateInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkProcessFormInfoRelations_WorkProcessInfos_BelongWorkProcessId",
                        column: x => x.BelongWorkProcessId,
                        principalTable: "WorkProcessInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLines",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    ProductLineName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductLineNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BelongWorkShopId = table.Column<long>(type: "bigint", nullable: true),
                    ProductLineState = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLines_WorkShopInfos_BelongWorkShopId",
                        column: x => x.BelongWorkShopId,
                        principalTable: "WorkShopInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DynamicEntityPropertyValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DynamicEntityPropertyId = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicEntityPropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DynamicEntityPropertyValues_DynamicEntityProperties_DynamicEntityPropertyId",
                        column: x => x.DynamicEntityPropertyId,
                        principalTable: "DynamicEntityProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityPropertyChanges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityChangeId = table.Column<long>(type: "bigint", nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    OriginalValue = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    PropertyName = table.Column<string>(type: "nvarchar(96)", maxLength: 96, nullable: true),
                    PropertyTypeFullName = table.Column<string>(type: "nvarchar(192)", maxLength: 192, nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    NewValueHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalValueHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityPropertyChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityPropertyChanges_EntityChanges_EntityChangeId",
                        column: x => x.EntityChangeId,
                        principalTable: "EntityChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BomInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BomInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BomInfos_MaterialInfos_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "MaterialInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialBatchNumberRulers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialInfoId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GenerateType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSerailNumber = table.Column<bool>(type: "bit", nullable: false),
                    FlowNumberRuler = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    FlowNumberRulerLength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialBatchNumberRulers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialBatchNumberRulers_MaterialInfos_MaterialInfoId",
                        column: x => x.MaterialInfoId,
                        principalTable: "MaterialInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderMaterialProduceStatuses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialBatchNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    MaterialInfoId = table.Column<long>(type: "bigint", nullable: true),
                    ProduceStatus = table.Column<int>(type: "int", nullable: false),
                    CurrentWorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    WorkProcessInfoId = table.Column<long>(type: "bigint", nullable: true),
                    LeftWorkProcessCount = table.Column<int>(type: "int", nullable: false),
                    PredictEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderMaterialProduceStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderMaterialProduceStatuses_MaterialInfos_MaterialInfoId",
                        column: x => x.MaterialInfoId,
                        principalTable: "MaterialInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderMaterialProduceStatuses_WorkProcessInfos_WorkProcessInfoId",
                        column: x => x.WorkProcessInfoId,
                        principalTable: "WorkProcessInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderBomItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BelongWorkOrderBomId = table.Column<long>(type: "bigint", nullable: false),
                    BelongWorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    InputMaterialId = table.Column<long>(type: "bigint", nullable: false),
                    InputMaterialInfoId = table.Column<long>(type: "bigint", nullable: true),
                    InputMaterialCount = table.Column<decimal>(type: "decimal(20,3)", precision: 20, scale: 3, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderBomItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderBomItems_MaterialInfos_InputMaterialInfoId",
                        column: x => x.InputMaterialInfoId,
                        principalTable: "MaterialInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkOrderBomItems_WorkProcessInfos_BelongWorkProcessId",
                        column: x => x.BelongWorkProcessId,
                        principalTable: "WorkProcessInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessSetBomItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BelongWorkProcessSetBomId = table.Column<long>(type: "bigint", nullable: false),
                    BelongWorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    InputMaterialId = table.Column<long>(type: "bigint", nullable: false),
                    InputMaterialCount = table.Column<decimal>(type: "decimal(20,3)", precision: 20, scale: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessSetBomItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkProcessSetBomItems_MaterialInfos_InputMaterialId",
                        column: x => x.InputMaterialId,
                        principalTable: "MaterialInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkProcessSetBomItems_WorkProcessInfos_BelongWorkProcessId",
                        column: x => x.BelongWorkProcessId,
                        principalTable: "WorkProcessInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessSetProductRelations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialInfoId = table.Column<long>(type: "bigint", nullable: false),
                    BelongWorkProcessSetId = table.Column<long>(type: "bigint", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessSetProductRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkProcessSetProductRelations_MaterialInfos_MaterialInfoId",
                        column: x => x.MaterialInfoId,
                        principalTable: "MaterialInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkProcessSetProductRelations_WorkProcessSets_BelongWorkProcessSetId",
                        column: x => x.BelongWorkProcessSetId,
                        principalTable: "WorkProcessSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsGranted = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLineUserRelations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserInfoId = table.Column<long>(type: "bigint", nullable: false),
                    ProductLineId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLineUserRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLineUserRelations_ProductLines_ProductLineId",
                        column: x => x.ProductLineId,
                        principalTable: "ProductLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLineUserRelations_Users_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FromOrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaterialInfoId = table.Column<long>(type: "bigint", nullable: false),
                    WorkOrderBomId = table.Column<long>(type: "bigint", nullable: false),
                    ProduceWorkShopId = table.Column<long>(type: "bigint", nullable: true),
                    ProduceLineId = table.Column<long>(type: "bigint", nullable: true),
                    ProduceCount = table.Column<decimal>(type: "decimal(20,3)", precision: 20, scale: 3, nullable: false),
                    PlanStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActrualStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActuralEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkOrderStatu = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderInfos_MaterialInfos_MaterialInfoId",
                        column: x => x.MaterialInfoId,
                        principalTable: "MaterialInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrderInfos_ProductLines_ProduceLineId",
                        column: x => x.ProduceLineId,
                        principalTable: "ProductLines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkOrderInfos_WorkShopInfos_ProduceWorkShopId",
                        column: x => x.ProduceWorkShopId,
                        principalTable: "WorkShopInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkStationInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    WorkStationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkStationNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BelongProductLineId = table.Column<long>(type: "bigint", nullable: true),
                    BelongWorkShopId = table.Column<long>(type: "bigint", nullable: true),
                    BelongWorkShopNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStationInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkStationInfos_ProductLines_BelongProductLineId",
                        column: x => x.BelongProductLineId,
                        principalTable: "ProductLines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkStationInfos_WorkShopInfos_BelongWorkShopId",
                        column: x => x.BelongWorkShopId,
                        principalTable: "WorkShopInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BomItemInfos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BelongBomId = table.Column<long>(type: "bigint", nullable: false),
                    FormMaterialId = table.Column<long>(type: "bigint", nullable: false),
                    FormMaterialNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FormMaterialName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FormCount = table.Column<decimal>(type: "decimal(20,3)", precision: 20, scale: 3, nullable: false),
                    LossFactor = table.Column<decimal>(type: "decimal(20,3)", precision: 20, scale: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BomItemInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BomItemInfos_BomInfos_BelongBomId",
                        column: x => x.BelongBomId,
                        principalTable: "BomInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessSetBoms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BelongWorkProcessSetId = table.Column<long>(type: "bigint", nullable: false),
                    ReferenceBomId = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessSetBoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkProcessSetBoms_BomInfos_ReferenceBomId",
                        column: x => x.ReferenceBomId,
                        principalTable: "BomInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkProcessSetBoms_WorkProcessSets_BelongWorkProcessSetId",
                        column: x => x.BelongWorkProcessSetId,
                        principalTable: "WorkProcessSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkProcessStationRelations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BelongWorkStationId = table.Column<long>(type: "bigint", nullable: false),
                    BelongWorkProcessId = table.Column<long>(type: "bigint", nullable: false),
                    CreatTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkProcessStationRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkProcessStationRelations_WorkProcessInfos_BelongWorkProcessId",
                        column: x => x.BelongWorkProcessId,
                        principalTable: "WorkProcessInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkProcessStationRelations_WorkStationInfos_BelongWorkStationId",
                        column: x => x.BelongWorkStationId,
                        principalTable: "WorkStationInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkStationUserRelations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserInfoId = table.Column<long>(type: "bigint", nullable: false),
                    WorkStationInfoId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkStationUserRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkStationUserRelations_Users_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkStationUserRelations_WorkStationInfos_WorkStationInfoId",
                        column: x => x.WorkStationInfoId,
                        principalTable: "WorkStationInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderBoms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderId = table.Column<long>(type: "bigint", nullable: false),
                    WorkOrderNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MaterialId = table.Column<long>(type: "bigint", nullable: false),
                    WorkProcessSetBomId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderBoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderBoms_WorkOrderInfos_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrderInfos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkOrderBoms_WorkProcessSetBoms_WorkProcessSetBomId",
                        column: x => x.WorkProcessSetBomId,
                        principalTable: "WorkProcessSetBoms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TenantId_ExecutionDuration",
                table: "AuditLogs",
                columns: new[] { "TenantId", "ExecutionDuration" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TenantId_ExecutionTime",
                table: "AuditLogs",
                columns: new[] { "TenantId", "ExecutionTime" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TenantId_UserId",
                table: "AuditLogs",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_BackgroundJobs_IsAbandoned_NextTryTime",
                table: "BackgroundJobs",
                columns: new[] { "IsAbandoned", "NextTryTime" });

            migrationBuilder.CreateIndex(
                name: "IX_BomInfos_MaterialId",
                table: "BomInfos",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_BomItemInfos_BelongBomId",
                table: "BomItemInfos",
                column: "BelongBomId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicEntityProperties_DynamicPropertyId",
                table: "DynamicEntityProperties",
                column: "DynamicPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicEntityProperties_EntityFullName_DynamicPropertyId_TenantId",
                table: "DynamicEntityProperties",
                columns: new[] { "EntityFullName", "DynamicPropertyId", "TenantId" },
                unique: true,
                filter: "[EntityFullName] IS NOT NULL AND [TenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicEntityPropertyValues_DynamicEntityPropertyId",
                table: "DynamicEntityPropertyValues",
                column: "DynamicEntityPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicProperties_PropertyName_TenantId",
                table: "DynamicProperties",
                columns: new[] { "PropertyName", "TenantId" },
                unique: true,
                filter: "[PropertyName] IS NOT NULL AND [TenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicPropertyValues_DynamicPropertyId",
                table: "DynamicPropertyValues",
                column: "DynamicPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityChanges_EntityChangeSetId",
                table: "EntityChanges",
                column: "EntityChangeSetId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityChanges_EntityTypeFullName_EntityId",
                table: "EntityChanges",
                columns: new[] { "EntityTypeFullName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityChangeSets_TenantId_CreationTime",
                table: "EntityChangeSets",
                columns: new[] { "TenantId", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityChangeSets_TenantId_Reason",
                table: "EntityChangeSets",
                columns: new[] { "TenantId", "Reason" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityChangeSets_TenantId_UserId",
                table: "EntityChangeSets",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_EntityPropertyChanges_EntityChangeId",
                table: "EntityPropertyChanges",
                column: "EntityChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ERPSyncTasks_CreateTime",
                table: "ERPSyncTasks",
                column: "CreateTime");

            migrationBuilder.CreateIndex(
                name: "IX_ERPSyncTasks_SyncState",
                table: "ERPSyncTasks",
                column: "SyncState");

            migrationBuilder.CreateIndex(
                name: "IX_ERPSyncTasks_SyncType",
                table: "ERPSyncTasks",
                column: "SyncType");

            migrationBuilder.CreateIndex(
                name: "IX_Features_EditionId_Name",
                table: "Features",
                columns: new[] { "EditionId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Features_TenantId_Name",
                table: "Features",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplateInfos_FormsName",
                table: "FormTemplateInfos",
                column: "FormsName");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_TenantId_Name",
                table: "Languages",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTexts_TenantId_Source_LanguageName_Key",
                table: "LanguageTexts",
                columns: new[] { "TenantId", "Source", "LanguageName", "Key" });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialBatchNumberRulers_MaterialInfoId",
                table: "MaterialBatchNumberRulers",
                column: "MaterialInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaterialCategories_CategoryCode",
                table: "MaterialCategories",
                column: "CategoryCode");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInfos_BelongCategoryId",
                table: "MaterialInfos",
                column: "BelongCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialInfos_MaterialNumber",
                table: "MaterialInfos",
                column: "MaterialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSubscriptions_NotificationName_EntityTypeName_EntityId_UserId",
                table: "NotificationSubscriptions",
                columns: new[] { "NotificationName", "EntityTypeName", "EntityId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationSubscriptions_TenantId_NotificationName_EntityTypeName_EntityId_UserId",
                table: "NotificationSubscriptions",
                columns: new[] { "TenantId", "NotificationName", "EntityTypeName", "EntityId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_MaterialBatchNumber",
                table: "OrderMaterialProduceStatuses",
                column: "MaterialBatchNumber");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_MaterialInfoId",
                table: "OrderMaterialProduceStatuses",
                column: "MaterialInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_ProduceStatus",
                table: "OrderMaterialProduceStatuses",
                column: "ProduceStatus");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_WorkOrderNumber",
                table: "OrderMaterialProduceStatuses",
                column: "WorkOrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_OrderMaterialProduceStatuses_WorkProcessInfoId",
                table: "OrderMaterialProduceStatuses",
                column: "WorkProcessInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnitRoles_TenantId_OrganizationUnitId",
                table: "OrganizationUnitRoles",
                columns: new[] { "TenantId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnitRoles_TenantId_RoleId",
                table: "OrganizationUnitRoles",
                columns: new[] { "TenantId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnits_ParentId",
                table: "OrganizationUnits",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnits_TenantId_Code",
                table: "OrganizationUnits",
                columns: new[] { "TenantId", "Code" });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_TenantId_Name",
                table: "Permissions",
                columns: new[] { "TenantId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_UserId",
                table: "Permissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLines_BelongWorkShopId",
                table: "ProductLines",
                column: "BelongWorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLines_ProductLineNumber",
                table: "ProductLines",
                column: "ProductLineNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLineUserRelations_ProductLineId",
                table: "ProductLineUserRelations",
                column: "ProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLineUserRelations_UserInfoId",
                table: "ProductLineUserRelations",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_QualityProblemDefines_ProblemCategoryId",
                table: "QualityProblemDefines",
                column: "ProblemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QualityProblemDefines_QualityProblemNumber",
                table: "QualityProblemDefines",
                column: "QualityProblemNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_TenantId_ClaimType",
                table: "RoleClaims",
                columns: new[] { "TenantId", "ClaimType" });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatorUserId",
                table: "Roles",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DeleterUserId",
                table: "Roles",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_LastModifierUserId",
                table: "Roles",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_TenantId_NormalizedName",
                table: "Roles",
                columns: new[] { "TenantId", "NormalizedName" });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_TenantId_Name_UserId",
                table: "Settings",
                columns: new[] { "TenantId", "Name", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Settings_UserId",
                table: "Settings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantNotifications_TenantId",
                table: "TenantNotifications",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_CreatorUserId",
                table: "Tenants",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_DeleterUserId",
                table: "Tenants",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_EditionId",
                table: "Tenants",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_LastModifierUserId",
                table: "Tenants",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_TenancyName",
                table: "Tenants",
                column: "TenancyName");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_EmailAddress",
                table: "UserAccounts",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_TenantId_EmailAddress",
                table: "UserAccounts",
                columns: new[] { "TenantId", "EmailAddress" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_TenantId_UserId",
                table: "UserAccounts",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_TenantId_UserName",
                table: "UserAccounts",
                columns: new[] { "TenantId", "UserName" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserName",
                table: "UserAccounts",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_TenantId_ClaimType",
                table: "UserClaims",
                columns: new[] { "TenantId", "ClaimType" });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginAttempts_TenancyName_UserNameOrEmailAddress_Result",
                table: "UserLoginAttempts",
                columns: new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginAttempts_UserId_TenantId",
                table: "UserLoginAttempts",
                columns: new[] { "UserId", "TenantId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_ProviderKey_TenantId",
                table: "UserLogins",
                columns: new[] { "ProviderKey", "TenantId" },
                unique: true,
                filter: "[TenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_TenantId_LoginProvider_ProviderKey",
                table: "UserLogins",
                columns: new[] { "TenantId", "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_TenantId_UserId",
                table: "UserLogins",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_UserId_State_CreationTime",
                table: "UserNotifications",
                columns: new[] { "UserId", "State", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganizationUnits_TenantId_OrganizationUnitId",
                table: "UserOrganizationUnits",
                columns: new[] { "TenantId", "OrganizationUnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserOrganizationUnits_TenantId_UserId",
                table: "UserOrganizationUnits",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_TenantId_RoleId",
                table: "UserRoles",
                columns: new[] { "TenantId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_TenantId_UserId",
                table: "UserRoles",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatorUserId",
                table: "Users",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DeleterUserId",
                table: "Users",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LastModifierUserId",
                table: "Users",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId_NormalizedEmailAddress",
                table: "Users",
                columns: new[] { "TenantId", "NormalizedEmailAddress" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId_NormalizedUserName",
                table: "Users",
                columns: new[] { "TenantId", "NormalizedUserName" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_TenantId_UserId",
                table: "UserTokens",
                columns: new[] { "TenantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WebhookSendAttempts_WebhookEventId",
                table: "WebhookSendAttempts",
                column: "WebhookEventId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderBomItems_BelongWorkProcessId",
                table: "WorkOrderBomItems",
                column: "BelongWorkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderBomItems_InputMaterialInfoId",
                table: "WorkOrderBomItems",
                column: "InputMaterialInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderBoms_MaterialId",
                table: "WorkOrderBoms",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderBoms_WorkOrderId",
                table: "WorkOrderBoms",
                column: "WorkOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderBoms_WorkOrderNumber",
                table: "WorkOrderBoms",
                column: "WorkOrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderBoms_WorkProcessSetBomId",
                table: "WorkOrderBoms",
                column: "WorkProcessSetBomId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderInfos_MaterialInfoId",
                table: "WorkOrderInfos",
                column: "MaterialInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderInfos_OrderNumber",
                table: "WorkOrderInfos",
                column: "OrderNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderInfos_PlanStartTime_PlanEndTime",
                table: "WorkOrderInfos",
                columns: new[] { "PlanStartTime", "PlanEndTime" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderInfos_ProduceLineId",
                table: "WorkOrderInfos",
                column: "ProduceLineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderInfos_ProduceWorkShopId",
                table: "WorkOrderInfos",
                column: "ProduceWorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderInfos_WorkOrderStatu",
                table: "WorkOrderInfos",
                column: "WorkOrderStatu");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessFormInfoRelations_BelongFormInfoId",
                table: "WorkProcessFormInfoRelations",
                column: "BelongFormInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessFormInfoRelations_BelongWorkProcessId",
                table: "WorkProcessFormInfoRelations",
                column: "BelongWorkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessSetBomItems_BelongWorkProcessId",
                table: "WorkProcessSetBomItems",
                column: "BelongWorkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessSetBomItems_InputMaterialId",
                table: "WorkProcessSetBomItems",
                column: "InputMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessSetBoms_BelongWorkProcessSetId",
                table: "WorkProcessSetBoms",
                column: "BelongWorkProcessSetId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessSetBoms_ReferenceBomId",
                table: "WorkProcessSetBoms",
                column: "ReferenceBomId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessSetProductRelations_BelongWorkProcessSetId",
                table: "WorkProcessSetProductRelations",
                column: "BelongWorkProcessSetId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessSetProductRelations_MaterialInfoId",
                table: "WorkProcessSetProductRelations",
                column: "MaterialInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessStationRelations_BelongWorkProcessId",
                table: "WorkProcessStationRelations",
                column: "BelongWorkProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkProcessStationRelations_BelongWorkStationId",
                table: "WorkProcessStationRelations",
                column: "BelongWorkStationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShopInfos_WorkShopNumber",
                table: "WorkShopInfos",
                column: "WorkShopNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkStationInfos_BelongProductLineId",
                table: "WorkStationInfos",
                column: "BelongProductLineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkStationInfos_BelongWorkShopId",
                table: "WorkStationInfos",
                column: "BelongWorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkStationInfos_WorkStationNumber",
                table: "WorkStationInfos",
                column: "WorkStationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkStationUserRelations_UserInfoId",
                table: "WorkStationUserRelations",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkStationUserRelations_WorkStationInfoId",
                table: "WorkStationUserRelations",
                column: "WorkStationInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BackgroundJobs");

            migrationBuilder.DropTable(
                name: "BomItemInfos");

            migrationBuilder.DropTable(
                name: "DynamicEntityPropertyValues");

            migrationBuilder.DropTable(
                name: "DynamicPropertyValues");

            migrationBuilder.DropTable(
                name: "EntityPropertyChanges");

            migrationBuilder.DropTable(
                name: "ERPSyncTasks");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "LanguageTexts");

            migrationBuilder.DropTable(
                name: "MaterialBatchNumberRulers");

            migrationBuilder.DropTable(
                name: "MaterialReplaceRelations");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificationSubscriptions");

            migrationBuilder.DropTable(
                name: "OrderMaterialProduceStatuses");

            migrationBuilder.DropTable(
                name: "OrganizationUnitRoles");

            migrationBuilder.DropTable(
                name: "OrganizationUnits");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "ProductLineUserRelations");

            migrationBuilder.DropTable(
                name: "QualityProblemDefines");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "TenantNotifications");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "UserAccounts");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLoginAttempts");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "UserOrganizationUnits");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "WebhookSendAttempts");

            migrationBuilder.DropTable(
                name: "WebhookSubscriptions");

            migrationBuilder.DropTable(
                name: "WorkOrderBomItems");

            migrationBuilder.DropTable(
                name: "WorkOrderBoms");

            migrationBuilder.DropTable(
                name: "WorkProcessFormInfoRelations");

            migrationBuilder.DropTable(
                name: "WorkProcessSetBomItems");

            migrationBuilder.DropTable(
                name: "WorkProcessSetProductRelations");

            migrationBuilder.DropTable(
                name: "WorkProcessStationRelations");

            migrationBuilder.DropTable(
                name: "WorkStationUserRelations");

            migrationBuilder.DropTable(
                name: "DynamicEntityProperties");

            migrationBuilder.DropTable(
                name: "EntityChanges");

            migrationBuilder.DropTable(
                name: "ProblemCategories");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Editions");

            migrationBuilder.DropTable(
                name: "WebhookEvents");

            migrationBuilder.DropTable(
                name: "WorkOrderInfos");

            migrationBuilder.DropTable(
                name: "WorkProcessSetBoms");

            migrationBuilder.DropTable(
                name: "FormTemplateInfos");

            migrationBuilder.DropTable(
                name: "WorkProcessInfos");

            migrationBuilder.DropTable(
                name: "WorkStationInfos");

            migrationBuilder.DropTable(
                name: "DynamicProperties");

            migrationBuilder.DropTable(
                name: "EntityChangeSets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BomInfos");

            migrationBuilder.DropTable(
                name: "WorkProcessSets");

            migrationBuilder.DropTable(
                name: "ProductLines");

            migrationBuilder.DropTable(
                name: "MaterialInfos");

            migrationBuilder.DropTable(
                name: "WorkShopInfos");

            migrationBuilder.DropTable(
                name: "MaterialCategories");
        }
    }
}
