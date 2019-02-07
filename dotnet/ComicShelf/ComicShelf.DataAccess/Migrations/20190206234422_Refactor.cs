using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicShelf.DataAccess.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserCollectionId",
                table: "ComicCollections",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserCollections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollectionId = table.Column<int>(nullable: false),
                    ComicCollectionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsPublic = table.Column<bool>(nullable: false),
                    UserCollectionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_UserCollections_UserCollectionId",
                        column: x => x.UserCollectionId,
                        principalTable: "UserCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComicCollections_UserCollectionId",
                table: "ComicCollections",
                column: "UserCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_Id",
                table: "Collections",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_UserCollectionId",
                table: "Collections",
                column: "UserCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCollections_Id",
                table: "UserCollections",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ComicCollections_UserCollections_UserCollectionId",
                table: "ComicCollections",
                column: "UserCollectionId",
                principalTable: "UserCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComicCollections_UserCollections_UserCollectionId",
                table: "ComicCollections");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "UserCollections");

            migrationBuilder.DropIndex(
                name: "IX_ComicCollections_UserCollectionId",
                table: "ComicCollections");

            migrationBuilder.DropColumn(
                name: "UserCollectionId",
                table: "ComicCollections");
        }
    }
}
