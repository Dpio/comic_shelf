using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComicShelf.DataAccess.Migrations
{
    public partial class ComicChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Comics");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Comics",
                newName: "PremierDate");

            migrationBuilder.AlterColumn<int>(
                name: "Issue",
                table: "Comics",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Comics",
                nullable: true,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "Comics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Draftsman",
                table: "Comics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScriptWriter",
                table: "Comics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "Comics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Translator",
                table: "Comics",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Volume",
                table: "Comics",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "Draftsman",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "ScriptWriter",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "Translator",
                table: "Comics");

            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Comics");

            migrationBuilder.RenameColumn(
                name: "PremierDate",
                table: "Comics",
                newName: "StartDate");

            migrationBuilder.AlterColumn<string>(
                name: "Issue",
                table: "Comics",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<byte>(
                name: "Image",
                table: "Comics",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Comics",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
