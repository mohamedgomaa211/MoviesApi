using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesApi.Migrations
{
    /// <inheritdoc />
    public partial class updatemoive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_moives_Genres_GenreId1",
                table: "moives");

            migrationBuilder.DropIndex(
                name: "IX_moives_GenreId1",
                table: "moives");

            migrationBuilder.DropColumn(
                name: "GenreId1",
                table: "moives");

            migrationBuilder.AlterColumn<byte>(
                name: "GenreId",
                table: "moives",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_moives_GenreId",
                table: "moives",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_moives_Genres_GenreId",
                table: "moives",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_moives_Genres_GenreId",
                table: "moives");

            migrationBuilder.DropIndex(
                name: "IX_moives_GenreId",
                table: "moives");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "moives",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<byte>(
                name: "GenreId1",
                table: "moives",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_moives_GenreId1",
                table: "moives",
                column: "GenreId1");

            migrationBuilder.AddForeignKey(
                name: "FK_moives_Genres_GenreId1",
                table: "moives",
                column: "GenreId1",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
