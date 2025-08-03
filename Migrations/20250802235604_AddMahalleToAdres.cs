using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IkinciElKitapProjesi.Migrations
{
    /// <inheritdoc />
    public partial class AddMahalleToAdres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitaplar_Kategoriler_KategoriID",
                table: "Kitaplar");

            migrationBuilder.AlterColumn<int>(
                name: "KategoriID",
                table: "Kitaplar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ilce",
                table: "Adresler",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Il",
                table: "Adresler",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Mahalle",
                table: "Adresler",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Kitaplar_Kategoriler_KategoriID",
                table: "Kitaplar",
                column: "KategoriID",
                principalTable: "Kategoriler",
                principalColumn: "KategoriID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kitaplar_Kategoriler_KategoriID",
                table: "Kitaplar");

            migrationBuilder.DropColumn(
                name: "Mahalle",
                table: "Adresler");

            migrationBuilder.AlterColumn<int>(
                name: "KategoriID",
                table: "Kitaplar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Ilce",
                table: "Adresler",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Il",
                table: "Adresler",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Kitaplar_Kategoriler_KategoriID",
                table: "Kitaplar",
                column: "KategoriID",
                principalTable: "Kategoriler",
                principalColumn: "KategoriID");
        }
    }
}
