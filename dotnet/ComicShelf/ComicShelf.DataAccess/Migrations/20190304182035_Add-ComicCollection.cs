using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicShelf.DataAccess.Migrations
{
    public partial class AddComicCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CollectionId",
                table: "Comics");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Collections",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ComicCollections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollectionId = table.Column<int>(nullable: false),
                    ComicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComicCollections_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComicCollections_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComicCollections_CollectionId",
                table: "ComicCollections",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_ComicCollections_ComicId",
                table: "ComicCollections",
                column: "ComicId");

            migrationBuilder.CreateIndex(
                name: "IX_ComicCollections_Id",
                table: "ComicCollections",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Users_UserId",
                table: "Collections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Users_UserId",
                table: "Collections");

            migrationBuilder.DropTable(
                name: "ComicCollections");

            migrationBuilder.AddColumn<int>(
                name: "CollectionId",
                table: "Comics",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Collections",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Comics_CollectionId",
                table: "Comics",
                column: "CollectionId");

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
    }
}
