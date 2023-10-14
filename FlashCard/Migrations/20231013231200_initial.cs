using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlashCard.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
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
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
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
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    ThemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThemeName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.ThemeId);
                    table.ForeignKey(
                        name: "FK_Themes_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WordText = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ThemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordId);
                    table.ForeignKey(
                        name: "FK_Words_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Words_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Words_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "ThemeId");
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    TranslationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceWordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetWordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.TranslationId);
                    table.ForeignKey(
                        name: "FK_Translations_Words_SourceWordId",
                        column: x => x.SourceWordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Translations_Words_TargetWordId",
                        column: x => x.TargetWordId,
                        principalTable: "Words",
                        principalColumn: "WordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "LanguageName" },
                values: new object[,]
                {
                    { new Guid("14e4d292-a580-4a4d-9b0c-ed49a85179d2"), "Spanish" },
                    { new Guid("33526ce8-8842-49d6-8780-ba61be8069ab"), "French" },
                    { new Guid("54eae254-972b-4ce1-b08b-192f1ac2567f"), "English" },
                    { new Guid("8703ae8f-2886-46e9-a0ec-4a28b29ceb5a"), "German" },
                    { new Guid("92965b17-28a6-4336-b13f-d1ff1c1b1444"), "Italian" },
                    { new Guid("c840e6bf-2cfb-4eaf-8322-95bc9f70b975"), "Russian" }
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "LevelId", "LevelName" },
                values: new object[,]
                {
                    { new Guid("026d2af9-62f1-441f-ab29-23ad4a1aae97"), "B2" },
                    { new Guid("47538f2b-04ee-4bcd-bc2e-810d462081bd"), "B1" },
                    { new Guid("4802a577-ed4c-4646-ad2b-b9604ceac574"), "A2" },
                    { new Guid("495996f4-ee53-4769-a942-832a706b8178"), "A1" },
                    { new Guid("ebd87f8f-2dfa-482d-bbb8-17d6ba5d2637"), "C2" },
                    { new Guid("f3323772-4d35-4d27-a1a3-f86453c5e6df"), "C1" }
                });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "ThemeId", "LanguageId", "ThemeName" },
                values: new object[,]
                {
                    { new Guid("0c8bde03-0e26-4581-8b24-9904f6fc133b"), new Guid("33526ce8-8842-49d6-8780-ba61be8069ab"), "Générale" },
                    { new Guid("3d9051b5-78c2-4e4a-991a-ab7fa22503b7"), new Guid("92965b17-28a6-4336-b13f-d1ff1c1b1444"), "Generale" },
                    { new Guid("6c4a0485-c3d2-4e1b-888c-78ae793a8e30"), new Guid("14e4d292-a580-4a4d-9b0c-ed49a85179d2"), "General" },
                    { new Guid("6d325e08-b371-4e56-9dec-079013a7218b"), new Guid("54eae254-972b-4ce1-b08b-192f1ac2567f"), "General" },
                    { new Guid("9c1ccaaf-0e15-493e-b349-c6fe18f7537d"), new Guid("c840e6bf-2cfb-4eaf-8322-95bc9f70b975"), "Общее" },
                    { new Guid("a4f5698f-de36-4334-ab4f-7e3b5d58f23b"), new Guid("8703ae8f-2886-46e9-a0ec-4a28b29ceb5a"), "Allgemein" }
                });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "WordId", "ImageUrl", "LanguageId", "LevelId", "ThemeId", "WordText" },
                values: new object[,]
                {
                    { new Guid("11237266-725e-45b2-b426-7abee30acfde"), "", new Guid("54eae254-972b-4ce1-b08b-192f1ac2567f"), new Guid("495996f4-ee53-4769-a942-832a706b8178"), new Guid("6d325e08-b371-4e56-9dec-079013a7218b"), "Hello" },
                    { new Guid("b977b0c1-1284-4f3a-8329-9040965386ad"), "", new Guid("c840e6bf-2cfb-4eaf-8322-95bc9f70b975"), new Guid("495996f4-ee53-4769-a942-832a706b8178"), new Guid("9c1ccaaf-0e15-493e-b349-c6fe18f7537d"), "Привет" },
                    { new Guid("cd2225d1-a9d1-454f-af85-eac853bbd45c"), "", new Guid("92965b17-28a6-4336-b13f-d1ff1c1b1444"), new Guid("495996f4-ee53-4769-a942-832a706b8178"), new Guid("3d9051b5-78c2-4e4a-991a-ab7fa22503b7"), "Ciao" },
                    { new Guid("df279714-8907-4800-ac56-2ea35b84f467"), "", new Guid("33526ce8-8842-49d6-8780-ba61be8069ab"), new Guid("495996f4-ee53-4769-a942-832a706b8178"), new Guid("0c8bde03-0e26-4581-8b24-9904f6fc133b"), "Bonjour" },
                    { new Guid("f96065c6-6d30-430a-8c65-3ce715d6574c"), "", new Guid("8703ae8f-2886-46e9-a0ec-4a28b29ceb5a"), new Guid("495996f4-ee53-4769-a942-832a706b8178"), new Guid("a4f5698f-de36-4334-ab4f-7e3b5d58f23b"), "Guten Tag" },
                    { new Guid("fe8dcbad-1001-4d63-8eca-a97bef5ec6ec"), "", new Guid("14e4d292-a580-4a4d-9b0c-ed49a85179d2"), new Guid("495996f4-ee53-4769-a942-832a706b8178"), new Guid("9c1ccaaf-0e15-493e-b349-c6fe18f7537d"), "Hola" }
                });

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "TranslationId", "SourceWordId", "TargetWordId" },
                values: new object[,]
                {
                    { new Guid("1ebac63e-6f19-4e3c-88d0-383083be1336"), new Guid("11237266-725e-45b2-b426-7abee30acfde"), new Guid("b977b0c1-1284-4f3a-8329-9040965386ad") },
                    { new Guid("45657d21-a8ef-4ea6-87a9-208a201b4f98"), new Guid("f96065c6-6d30-430a-8c65-3ce715d6574c"), new Guid("fe8dcbad-1001-4d63-8eca-a97bef5ec6ec") },
                    { new Guid("bdabcef1-c344-47d3-a4a8-e23fc03a1200"), new Guid("b977b0c1-1284-4f3a-8329-9040965386ad"), new Guid("11237266-725e-45b2-b426-7abee30acfde") },
                    { new Guid("ee963a82-4a19-49e4-84aa-8939ea4b9237"), new Guid("11237266-725e-45b2-b426-7abee30acfde"), new Guid("df279714-8907-4800-ac56-2ea35b84f467") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Themes_LanguageId_ThemeName",
                table: "Themes",
                columns: new[] { "LanguageId", "ThemeName" },
                unique: true,
                filter: "[ThemeName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_SourceWordId_TargetWordId",
                table: "Translations",
                columns: new[] { "SourceWordId", "TargetWordId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Translations_TargetWordId",
                table: "Translations",
                column: "TargetWordId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_LanguageId_ThemeId_WordText",
                table: "Words",
                columns: new[] { "LanguageId", "ThemeId", "WordText" },
                unique: true,
                filter: "[WordText] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Words_LevelId",
                table: "Words",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_ThemeId",
                table: "Words",
                column: "ThemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
