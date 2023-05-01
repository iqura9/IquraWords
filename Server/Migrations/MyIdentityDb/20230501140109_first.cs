using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations.MyIdentityDb
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Short_Name",
                table: "Language",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Short_Name",
                table: "Language");
        }
    }
}
