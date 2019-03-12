using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicShelf.DataAccess.Migrations
{
    public partial class AddRent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GiverId = table.Column<int>(nullable: false),
                    ReceiverId = table.Column<int>(nullable: false),
                    ComicId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rents_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rents_Users_GiverId",
                        column: x => x.GiverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rents_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rents_ComicId",
                table: "Rents",
                column: "ComicId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_GiverId",
                table: "Rents",
                column: "GiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_Id",
                table: "Rents",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rents_ReceiverId",
                table: "Rents",
                column: "ReceiverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rents");
        }
    }
}
