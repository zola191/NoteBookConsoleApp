using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoteBookApp.Migrations
{
    /// <inheritdoc />
    public partial class Azaza1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Azaza",
                table: "NoteBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Azaza",
                table: "NoteBooks");
        }
    }
}
