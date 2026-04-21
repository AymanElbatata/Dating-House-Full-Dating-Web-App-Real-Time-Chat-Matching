using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AYMDatingCore.DAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BDataSchema");

            migrationBuilder.EnsureSchema(
                name: "ASecurity");

            migrationBuilder.CreateTable(
                name: "AdminPanelTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PanelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activation = table.Column<bool>(type: "bit", nullable: false),
                    UserViewsCounter = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminPanelTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AppErrorTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppErrorTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContactUsTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CountryTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EducationTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmailTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FinancialModeTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialModeTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GenderTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LanguageTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatusTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatusTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PurposeTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurposeTBLs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryTBLId = table.Column<int>(type: "int", nullable: true),
                    GenderTBLId = table.Column<int>(type: "int", nullable: true),
                    DateOfJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    DateofBlockExpireIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_CountryTBLs_CountryTBLId",
                        column: x => x.CountryTBLId,
                        principalSchema: "BDataSchema",
                        principalTable: "CountryTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_GenderTBLs_GenderTBLId",
                        column: x => x.GenderTBLId,
                        principalSchema: "BDataSchema",
                        principalTable: "GenderTBLs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "ASecurity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAddressListTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Browser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddressListTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserAddressListTBLs_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserBlockTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBlockTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserBlockTBLs_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBlockTBLs_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "ASecurity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavorites",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorites", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserFavorites_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFavorites_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserHistoryTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    LanguageId = table.Column<int>(type: "int", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: true),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: true),
                    ProfessionId = table.Column<int>(type: "int", nullable: true),
                    PurposeId = table.Column<int>(type: "int", nullable: true),
                    FinancialModeId = table.Column<int>(type: "int", nullable: true),
                    EducationId = table.Column<int>(type: "int", nullable: true),
                    MainImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileHeading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutYou = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutPartner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    IsSwitchedOff = table.Column<bool>(type: "bit", nullable: false),
                    SearchAgeFrom = table.Column<int>(type: "int", nullable: true),
                    SearchAgeTo = table.Column<int>(type: "int", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHistoryTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserHistoryTBLs_CountryTBLs_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "BDataSchema",
                        principalTable: "CountryTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistoryTBLs_EducationTBLs_EducationId",
                        column: x => x.EducationId,
                        principalSchema: "BDataSchema",
                        principalTable: "EducationTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistoryTBLs_FinancialModeTBLs_FinancialModeId",
                        column: x => x.FinancialModeId,
                        principalSchema: "BDataSchema",
                        principalTable: "FinancialModeTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistoryTBLs_GenderTBLs_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "BDataSchema",
                        principalTable: "GenderTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistoryTBLs_LanguageTBLs_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "BDataSchema",
                        principalTable: "LanguageTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistoryTBLs_MaritalStatusTBLs_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalSchema: "BDataSchema",
                        principalTable: "MaritalStatusTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistoryTBLs_ProfessionTBLs_ProfessionId",
                        column: x => x.ProfessionId,
                        principalSchema: "BDataSchema",
                        principalTable: "ProfessionTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistoryTBLs_PurposeTBLs_PurposeId",
                        column: x => x.PurposeId,
                        principalSchema: "BDataSchema",
                        principalTable: "PurposeTBLs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserHistoryTBLs_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserImageTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImageTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserImageTBLs_Users_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLikeTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikeTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserLikeTBLs_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLikeTBLs_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "ASecurity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMessageGroupTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessageGroupTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserMessageGroupTBLs_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMessageGroupTBLs_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserMessageTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AudioDataUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileDataUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    IsDeletedFromSender = table.Column<bool>(type: "bit", nullable: false),
                    IsDeletedFromReceiver = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessageTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserMessageTBLs_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMessageTBLs_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserReportTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Complaint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReportTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserReportTBLs_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserReportTBLs_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "ASecurity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "ASecurity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "ASecurity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserViewTBLs",
                schema: "BDataSchema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReceiverAppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdateUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserViewTBLs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserViewTBLs_Users_ReceiverAppUserId",
                        column: x => x.ReceiverAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserViewTBLs_Users_SenderAppUserId",
                        column: x => x.SenderAppUserId,
                        principalSchema: "ASecurity",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "ASecurity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "ASecurity",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddressListTBLs_AppUserId",
                schema: "BDataSchema",
                table: "UserAddressListTBLs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlockTBLs_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserBlockTBLs",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBlockTBLs_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserBlockTBLs",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "ASecurity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserFavorites",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserFavorites",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistoryTBLs_AppUserId",
                schema: "BDataSchema",
                table: "UserHistoryTBLs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistoryTBLs_CountryId",
                schema: "BDataSchema",
                table: "UserHistoryTBLs",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistoryTBLs_EducationId",
                schema: "BDataSchema",
                table: "UserHistoryTBLs",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistoryTBLs_FinancialModeId",
                schema: "BDataSchema",
                table: "UserHistoryTBLs",
                column: "FinancialModeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistoryTBLs_GenderId",
                schema: "BDataSchema",
                table: "UserHistoryTBLs",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistoryTBLs_LanguageId",
                schema: "BDataSchema",
                table: "UserHistoryTBLs",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistoryTBLs_MaritalStatusId",
                schema: "BDataSchema",
                table: "UserHistoryTBLs",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistoryTBLs_ProfessionId",
                schema: "BDataSchema",
                table: "UserHistoryTBLs",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistoryTBLs_PurposeId",
                schema: "BDataSchema",
                table: "UserHistoryTBLs",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImageTBLs_AppUserId",
                schema: "BDataSchema",
                table: "UserImageTBLs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikeTBLs_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserLikeTBLs",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikeTBLs_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserLikeTBLs",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "ASecurity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessageGroupTBLs_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserMessageGroupTBLs",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessageGroupTBLs_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserMessageGroupTBLs",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessageTBLs_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserMessageTBLs",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessageTBLs_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserMessageTBLs",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReportTBLs_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserReportTBLs",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReportTBLs_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserReportTBLs",
                column: "SenderAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "ASecurity",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "ASecurity",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CountryTBLId",
                schema: "ASecurity",
                table: "Users",
                column: "CountryTBLId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GenderTBLId",
                schema: "ASecurity",
                table: "Users",
                column: "GenderTBLId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "ASecurity",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserViewTBLs_ReceiverAppUserId",
                schema: "BDataSchema",
                table: "UserViewTBLs",
                column: "ReceiverAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserViewTBLs_SenderAppUserId",
                schema: "BDataSchema",
                table: "UserViewTBLs",
                column: "SenderAppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminPanelTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "AppErrorTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "ContactUsTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "EmailTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserAddressListTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserBlockTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserFavorites",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserHistoryTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserImageTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserLikeTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserMessageGroupTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserMessageTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserReportTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "UserViewTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "EducationTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "FinancialModeTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "LanguageTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "MaritalStatusTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "ProfessionTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "PurposeTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "ASecurity");

            migrationBuilder.DropTable(
                name: "CountryTBLs",
                schema: "BDataSchema");

            migrationBuilder.DropTable(
                name: "GenderTBLs",
                schema: "BDataSchema");
        }
    }
}
