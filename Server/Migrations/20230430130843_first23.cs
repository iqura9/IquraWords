using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class first23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Word_Language_LanguageId",
                table: "Word");

            migrationBuilder.DropForeignKey(
                name: "FK_WordMeaning_Cluster_ClusterId",
                table: "WordMeaning");

            migrationBuilder.DropIndex(
                name: "IX_WordMeaning_ClusterId",
                table: "WordMeaning");

            migrationBuilder.DropIndex(
                name: "IX_Word_LanguageId",
                table: "Word");

            migrationBuilder.DropColumn(
                name: "ClusterId",
                table: "WordMeaning");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Word");

            migrationBuilder.CreateIndex(
                name: "IX_WordMeaning_Cluster_Id",
                table: "WordMeaning",
                column: "Cluster_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Word_Language_Id",
                table: "Word",
                column: "Language_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Word_Language_Language_Id",
                table: "Word",
                column: "Language_Id",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WordMeaning_Cluster_Cluster_Id",
                table: "WordMeaning",
                column: "Cluster_Id",
                principalTable: "Cluster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Word_Language_Language_Id",
                table: "Word");

            migrationBuilder.DropForeignKey(
                name: "FK_WordMeaning_Cluster_Cluster_Id",
                table: "WordMeaning");

            migrationBuilder.DropIndex(
                name: "IX_WordMeaning_Cluster_Id",
                table: "WordMeaning");

            migrationBuilder.DropIndex(
                name: "IX_Word_Language_Id",
                table: "Word");

            migrationBuilder.AddColumn<int>(
                name: "ClusterId",
                table: "WordMeaning",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Word",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_WordMeaning_ClusterId",
                table: "WordMeaning",
                column: "ClusterId");

            migrationBuilder.CreateIndex(
                name: "IX_Word_LanguageId",
                table: "Word",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Word_Language_LanguageId",
                table: "Word",
                column: "LanguageId",
                principalTable: "Language",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WordMeaning_Cluster_ClusterId",
                table: "WordMeaning",
                column: "ClusterId",
                principalTable: "Cluster",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
