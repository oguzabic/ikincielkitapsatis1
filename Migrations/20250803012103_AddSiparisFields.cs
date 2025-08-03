using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IkinciElKitapProjesi.Migrations
{
    /// <inheritdoc />
    public partial class AddSiparisFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mahalle",
                table: "Adresler");

            migrationBuilder.AddColumn<int>(
                name: "AdresID",
                table: "Siparisler",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Durum",
                table: "Siparisler",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "KartID",
                table: "Siparisler",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OdemeYontemi",
                table: "Siparisler",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Kartlar",
                keyColumn: "KartTipi",
                keyValue: null,
                column: "KartTipi",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "KartTipi",
                table: "Kartlar",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "KartNumarasi",
                table: "Kartlar",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(16)",
                oldMaxLength: 16)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_AdresID",
                table: "Siparisler",
                column: "AdresID");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_KartID",
                table: "Siparisler",
                column: "KartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Siparisler_Adresler_AdresID",
                table: "Siparisler",
                column: "AdresID",
                principalTable: "Adresler",
                principalColumn: "AdresID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Siparisler_Kartlar_KartID",
                table: "Siparisler",
                column: "KartID",
                principalTable: "Kartlar",
                principalColumn: "KartID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Siparisler_Adresler_AdresID",
                table: "Siparisler");

            migrationBuilder.DropForeignKey(
                name: "FK_Siparisler_Kartlar_KartID",
                table: "Siparisler");

            migrationBuilder.DropIndex(
                name: "IX_Siparisler_AdresID",
                table: "Siparisler");

            migrationBuilder.DropIndex(
                name: "IX_Siparisler_KartID",
                table: "Siparisler");

            migrationBuilder.DropColumn(
                name: "AdresID",
                table: "Siparisler");

            migrationBuilder.DropColumn(
                name: "Durum",
                table: "Siparisler");

            migrationBuilder.DropColumn(
                name: "KartID",
                table: "Siparisler");

            migrationBuilder.DropColumn(
                name: "OdemeYontemi",
                table: "Siparisler");

            migrationBuilder.AlterColumn<string>(
                name: "KartTipi",
                table: "Kartlar",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "KartNumarasi",
                table: "Kartlar",
                type: "varchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Mahalle",
                table: "Adresler",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
