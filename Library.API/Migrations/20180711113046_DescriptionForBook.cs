using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.API.Migrations
{
    public partial class DescriptionForBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "books");
        }
    }
}
