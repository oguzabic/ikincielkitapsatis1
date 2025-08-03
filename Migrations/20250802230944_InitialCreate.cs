using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IkinciElKitapProjesi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    KategoriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KategoriAdi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.KategoriID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ad = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Soyad = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sifre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.KullaniciID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kitaplar",
                columns: table => new
                {
                    KitapID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Baslik = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Yazar = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Yayinevi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Yil = table.Column<int>(type: "int", nullable: true),
                    Aciklama = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KategoriID = table.Column<int>(type: "int", nullable: true),
                    KategoriID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kitaplar", x => x.KitapID);
                    table.ForeignKey(
                        name: "FK_Kitaplar_Kategoriler_KategoriID",
                        column: x => x.KategoriID,
                        principalTable: "Kategoriler",
                        principalColumn: "KategoriID");
                    table.ForeignKey(
                        name: "FK_Kitaplar_Kategoriler_KategoriID1",
                        column: x => x.KategoriID1,
                        principalTable: "Kategoriler",
                        principalColumn: "KategoriID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Adresler",
                columns: table => new
                {
                    AdresID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KullaniciID = table.Column<int>(type: "int", nullable: false),
                    Baslik = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Il = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ilce = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AdresDetay = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostaKodu = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KullaniciID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresler", x => x.AdresID);
                    table.ForeignKey(
                        name: "FK_Adresler_Kullanicilar_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adresler_Kullanicilar_KullaniciID1",
                        column: x => x.KullaniciID1,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Kartlar",
                columns: table => new
                {
                    KartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KullaniciID = table.Column<int>(type: "int", nullable: false),
                    KartNumarasi = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SonKullanmaTarihi = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CVV = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KartTipi = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    KullaniciID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kartlar", x => x.KartID);
                    table.ForeignKey(
                        name: "FK_Kartlar_Kullanicilar_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kartlar_Kullanicilar_KullaniciID1",
                        column: x => x.KullaniciID1,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ilanlar",
                columns: table => new
                {
                    IlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KitapID = table.Column<int>(type: "int", nullable: false),
                    SaticiID = table.Column<int>(type: "int", nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Durum = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ResimYolu = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilanlar", x => x.IlanID);
                    table.ForeignKey(
                        name: "FK_Ilanlar_Kitaplar_KitapID",
                        column: x => x.KitapID,
                        principalTable: "Kitaplar",
                        principalColumn: "KitapID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ilanlar_Kullanicilar_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciID");
                    table.ForeignKey(
                        name: "FK_Ilanlar_Kullanicilar_SaticiID",
                        column: x => x.SaticiID,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Yorumlar",
                columns: table => new
                {
                    YorumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KitapID = table.Column<int>(type: "int", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: false),
                    YorumMetni = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Puan = table.Column<int>(type: "int", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    KullaniciID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yorumlar", x => x.YorumID);
                    table.ForeignKey(
                        name: "FK_Yorumlar_Kitaplar_KitapID",
                        column: x => x.KitapID,
                        principalTable: "Kitaplar",
                        principalColumn: "KitapID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Yorumlar_Kullanicilar_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Yorumlar_Kullanicilar_KullaniciID1",
                        column: x => x.KullaniciID1,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Siparisler",
                columns: table => new
                {
                    SiparisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IlanID = table.Column<int>(type: "int", nullable: false),
                    AliciID = table.Column<int>(type: "int", nullable: false),
                    SiparisTarihi = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siparisler", x => x.SiparisID);
                    table.ForeignKey(
                        name: "FK_Siparisler_Ilanlar_IlanID",
                        column: x => x.IlanID,
                        principalTable: "Ilanlar",
                        principalColumn: "IlanID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siparisler_Kullanicilar_AliciID",
                        column: x => x.AliciID,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Siparisler_Kullanicilar_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanicilar",
                        principalColumn: "KullaniciID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Adresler_KullaniciID",
                table: "Adresler",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Adresler_KullaniciID1",
                table: "Adresler",
                column: "KullaniciID1");

            migrationBuilder.CreateIndex(
                name: "IX_Ilanlar_KitapID",
                table: "Ilanlar",
                column: "KitapID");

            migrationBuilder.CreateIndex(
                name: "IX_Ilanlar_KullaniciID",
                table: "Ilanlar",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Ilanlar_SaticiID",
                table: "Ilanlar",
                column: "SaticiID");

            migrationBuilder.CreateIndex(
                name: "IX_Kartlar_KullaniciID",
                table: "Kartlar",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Kartlar_KullaniciID1",
                table: "Kartlar",
                column: "KullaniciID1");

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_KategoriID",
                table: "Kitaplar",
                column: "KategoriID");

            migrationBuilder.CreateIndex(
                name: "IX_Kitaplar_KategoriID1",
                table: "Kitaplar",
                column: "KategoriID1");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_AliciID",
                table: "Siparisler",
                column: "AliciID");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_IlanID",
                table: "Siparisler",
                column: "IlanID");

            migrationBuilder.CreateIndex(
                name: "IX_Siparisler_KullaniciID",
                table: "Siparisler",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_KitapID",
                table: "Yorumlar",
                column: "KitapID");

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_KullaniciID",
                table: "Yorumlar",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_KullaniciID1",
                table: "Yorumlar",
                column: "KullaniciID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adresler");

            migrationBuilder.DropTable(
                name: "Kartlar");

            migrationBuilder.DropTable(
                name: "Siparisler");

            migrationBuilder.DropTable(
                name: "Yorumlar");

            migrationBuilder.DropTable(
                name: "Ilanlar");

            migrationBuilder.DropTable(
                name: "Kitaplar");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Kategoriler");
        }
    }
}
