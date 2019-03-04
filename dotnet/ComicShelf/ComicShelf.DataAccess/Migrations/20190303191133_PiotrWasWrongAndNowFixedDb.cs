using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicShelf.DataAccess.Migrations
{
    public partial class PiotrWasWrongAndNowFixedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCollections");

            migrationBuilder.DropTable(
                name: "ComicCollections");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Comics",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWantList",
                table: "Collections",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Collections",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comics_CollectionId",
                table: "Comics",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_UserId",
                table: "Collections",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users_UserId",
                table: "Collections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comics_Collections_CollectionId",
                table: "Comics",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users_UserId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Comics_Collections_CollectionId",
                table: "Comics");

            migrationBuilder.DropIndex(
                name: "IX_Comics_CollectionId",
                table: "Comics");

            migrationBuilder.DropIndex(
                name: "IX_Collections_UserId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "IsWantList",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Collections");

            migrationBuilder.CreateTable(
                name: "ComicCollections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ComicId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComicCollections_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComicCollections_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    table.ForeignKey(
                        name: "FK_UserCollections_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCollections_ComicCollections_ComicCollectionId",
                        column: x => x.ComicCollectionId,
                        principalTable: "ComicCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComicCollections_ComicId",
                table: "ComicCollections",
                column: "ComicId");

            migrationBuilder.CreateIndex(
                name: "IX_ComicCollections_Id",
                table: "ComicCollections",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComicCollections_UserId",
                table: "ComicCollections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCollections_CollectionId",
                table: "UserCollections",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCollections_ComicCollectionId",
                table: "UserCollections",
                column: "ComicCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCollections_Id",
                table: "UserCollections",
                column: "Id",
                unique: true);
        }
    }
}
