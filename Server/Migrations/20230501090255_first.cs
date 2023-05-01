using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Registration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Avatar_url = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cluster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Collection_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cluster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cluster_Collection_Collection_Id",
                        column: x => x.Collection_Id,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Word",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Term = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Language_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Word", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Word_Language_Language_Id",
                        column: x => x.Language_Id,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    User_Id = table.Column<string>(type: "text", nullable: false),
                    Collection_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeTable_Collection_Collection_Id",
                        column: x => x.Collection_Id,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeTable_User_User_Id",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordMeaning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image_URL = table.Column<string>(type: "text", nullable: true),
                    Verified = table.Column<int>(type: "integer", nullable: false),
                    Term_Id = table.Column<int>(type: "integer", nullable: false),
                    Meaning_Id = table.Column<int>(type: "integer", nullable: false),
                    Cluster_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordMeaning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordMeaning_Cluster_Cluster_Id",
                        column: x => x.Cluster_Id,
                        principalTable: "Cluster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordMeaning_Word_Meaning_Id",
                        column: x => x.Meaning_Id,
                        principalTable: "Word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordMeaning_Word_Term_Id",
                        column: x => x.Term_Id,
                        principalTable: "Word",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Date_Added = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Date_Leaned = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    WordMeaning_id = table.Column<int>(type: "integer", nullable: false),
                    User_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWord_User_User_id",
                        column: x => x.User_id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWord_WordMeaning_WordMeaning_id",
                        column: x => x.WordMeaning_id,
                        principalTable: "WordMeaning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cluster_Collection_Id",
                table: "Cluster",
                column: "Collection_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LikeTable_Collection_Id",
                table: "LikeTable",
                column: "Collection_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LikeTable_User_Id",
                table: "LikeTable",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserWord_User_id",
                table: "UserWord",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserWord_WordMeaning_id",
                table: "UserWord",
                column: "WordMeaning_id");

            migrationBuilder.CreateIndex(
                name: "IX_Word_Language_Id",
                table: "Word",
                column: "Language_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WordMeaning_Cluster_Id",
                table: "WordMeaning",
                column: "Cluster_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WordMeaning_Meaning_Id",
                table: "WordMeaning",
                column: "Meaning_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WordMeaning_Term_Id",
                table: "WordMeaning",
                column: "Term_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeTable");

            migrationBuilder.DropTable(
                name: "UserWord");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "WordMeaning");

            migrationBuilder.DropTable(
                name: "Cluster");

            migrationBuilder.DropTable(
                name: "Word");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}
