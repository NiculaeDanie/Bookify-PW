using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Author_AuthorId1",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Book_BookId1",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Book_BookId1",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Genre_GenreId1",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_Book_BookId1",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Book_BookId1",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorites_BookId1",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserBook_BookId1",
                table: "UserBook");

            migrationBuilder.DropIndex(
                name: "IX_BookGenre_BookId1",
                table: "BookGenre");

            migrationBuilder.DropIndex(
                name: "IX_BookGenre_GenreId1",
                table: "BookGenre");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBook_AuthorId1",
                table: "AuthorBook");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBook_BookId1",
                table: "AuthorBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "UserFavorites");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "BookGenre");

            migrationBuilder.DropColumn(
                name: "GenreId1",
                table: "BookGenre");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "AuthorBook");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "AuthorBook");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "Authors");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "UserFavorites",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "UserBook",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GenreId",
                table: "BookGenre",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "BookGenre",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BookId",
                table: "AuthorBook",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "AuthorBook",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_BookId",
                table: "UserFavorites",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_BookId",
                table: "UserBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_BookId",
                table: "BookGenre",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_GenreId",
                table: "BookGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_AuthorId",
                table: "AuthorBook",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BookId",
                table: "AuthorBook",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Authors_AuthorId",
                table: "AuthorBook",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Books_BookId",
                table: "AuthorBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Books_BookId",
                table: "BookGenre",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Genres_GenreId",
                table: "BookGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_Books_BookId",
                table: "UserBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Books_BookId",
                table: "UserFavorites",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Authors_AuthorId",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_AuthorBook_Books_BookId",
                table: "AuthorBook");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Books_BookId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenre_Genres_GenreId",
                table: "BookGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_Books_BookId",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavorites_Books_BookId",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserFavorites_BookId",
                table: "UserFavorites");

            migrationBuilder.DropIndex(
                name: "IX_UserBook_BookId",
                table: "UserBook");

            migrationBuilder.DropIndex(
                name: "IX_BookGenre_BookId",
                table: "BookGenre");

            migrationBuilder.DropIndex(
                name: "IX_BookGenre_GenreId",
                table: "BookGenre");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBook_AuthorId",
                table: "AuthorBook");

            migrationBuilder.DropIndex(
                name: "IX_AuthorBook_BookId",
                table: "AuthorBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Book");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Author");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "UserFavorites",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "BookId1",
                table: "UserFavorites",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "UserBook",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "BookId1",
                table: "UserBook",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "BookGenre",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "BookGenre",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "BookId1",
                table: "BookGenre",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GenreId1",
                table: "BookGenre",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "BookId",
                table: "AuthorBook",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "AuthorBook",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId1",
                table: "AuthorBook",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookId1",
                table: "AuthorBook",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_BookId1",
                table: "UserFavorites",
                column: "BookId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_BookId1",
                table: "UserBook",
                column: "BookId1");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_BookId1",
                table: "BookGenre",
                column: "BookId1");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_GenreId1",
                table: "BookGenre",
                column: "GenreId1");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_AuthorId1",
                table: "AuthorBook",
                column: "AuthorId1");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BookId1",
                table: "AuthorBook",
                column: "BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Author_AuthorId1",
                table: "AuthorBook",
                column: "AuthorId1",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorBook_Book_BookId1",
                table: "AuthorBook",
                column: "BookId1",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Book_BookId1",
                table: "BookGenre",
                column: "BookId1",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenre_Genre_GenreId1",
                table: "BookGenre",
                column: "GenreId1",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_Book_BookId1",
                table: "UserBook",
                column: "BookId1",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavorites_Book_BookId1",
                table: "UserFavorites",
                column: "BookId1",
                principalTable: "Book",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
