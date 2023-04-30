using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class secnd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClusterId",
                table: "WordMeaning",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cluster_Id",
                table: "WordMeaning",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Language_Id",
                table: "Word",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Cluster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cluster", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordMeaning_ClusterId",
                table: "WordMeaning",
                column: "ClusterId");

            migrationBuilder.AddForeignKey(
                name: "FK_WordMeaning_Cluster_ClusterId",
                table: "WordMeaning",
                column: "ClusterId",
                principalTable: "Cluster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WordMeaning_Cluster_ClusterId",
                table: "WordMeaning");

            migrationBuilder.DropTable(
                name: "Cluster");

            migrationBuilder.DropIndex(
                name: "IX_WordMeaning_ClusterId",
                table: "WordMeaning");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "WordMeaning");

            migrationBuilder.DropColumn(
                name: "Cluster_Id",
                table: "WordMeaning");

            migrationBuilder.DropColumn(
                name: "Language_Id",
                table: "Word");
        }
    }
}
