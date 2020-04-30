using Microsoft.EntityFrameworkCore.Migrations;

namespace MemoPlus.Migrations
{
    public partial class AddMemoTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MemoTitle",
                table: "Memos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemoTitle",
                table: "Memos");
        }
    }
}
