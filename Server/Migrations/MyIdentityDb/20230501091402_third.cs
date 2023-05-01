using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations.MyIdentityDb
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Collection",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_User_Id",
                table: "Collection",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_AspNetUsers_User_Id",
                table: "Collection",
                column: "User_Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collection_AspNetUsers_User_Id",
                table: "Collection");

            migrationBuilder.DropIndex(
                name: "IX_Collection_User_Id",
                table: "Collection");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Collection");
        }
    }
}
