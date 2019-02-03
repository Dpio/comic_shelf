using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicShelf.DataAccess.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ComicCollections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ComicCollections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ComicCollections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ComicCollections",
                nullable: true);
        }
    }
}
