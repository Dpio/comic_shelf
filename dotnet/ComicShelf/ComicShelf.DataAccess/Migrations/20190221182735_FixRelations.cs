using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicShelf.DataAccess.Migrations
{
    public partial class FixRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_UserCollections_UserCollectionId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_ComicCollections_UserCollections_UserCollectionId",
                table: "ComicCollections");

            migrationBuilder.DropIndex(
                name: "IX_ComicCollections_UserCollectionId",
                table: "ComicCollections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_UserCollectionId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "UserCollectionId",
                table: "ComicCollections");

            migrationBuilder.DropColumn(
                name: "UserCollectionId",
                table: "Collections");

            migrationBuilder.CreateIndex(
                name: "IX_UserCollections_CollectionId",
                table: "UserCollections",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCollections_ComicCollectionId",
                table: "UserCollections",
                column: "ComicCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCollections_Collections_CollectionId",
                table: "UserCollections",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCollections_ComicCollections_ComicCollectionId",
                table: "UserCollections",
                column: "ComicCollectionId",
                principalTable: "ComicCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCollections_Collections_CollectionId",
                table: "UserCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCollections_ComicCollections_ComicCollectionId",
                table: "UserCollections");

            migrationBuilder.DropIndex(
                name: "IX_UserCollections_CollectionId",
                table: "UserCollections");

            migrationBuilder.DropIndex(
                name: "IX_UserCollections_ComicCollectionId",
                table: "UserCollections");

            migrationBuilder.AddColumn<int>(
                name: "UserCollectionId",
                table: "ComicCollections",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserCollectionId",
                table: "Collections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComicCollections_UserCollectionId",
                table: "ComicCollections",
                column: "UserCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_UserCollectionId",
                table: "Collections",
                column: "UserCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_UserCollections_UserCollectionId",
                table: "Collections",
                column: "UserCollectionId",
                principalTable: "UserCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComicCollections_UserCollections_UserCollectionId",
                table: "ComicCollections",
                column: "UserCollectionId",
                principalTable: "UserCollections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
