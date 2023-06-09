using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A_ZAcademy.Migrations
{
    /// <inheritdoc />
    public partial class mm2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "opinion",
                table: "clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "opinion",
                table: "clients");
        }
    }
}
