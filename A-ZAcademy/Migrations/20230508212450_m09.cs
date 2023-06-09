using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A_ZAcademy.Migrations
{
    /// <inheritdoc />
    public partial class m09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "categories");
        }
    }
}
