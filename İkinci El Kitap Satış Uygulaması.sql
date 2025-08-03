CREATE DATABASE IF NOT EXISTS IkinciElKitapDB
CHARACTER SET utf8mb4
COLLATE utf8mb4_general_ci;

SHOW DATABASES;

USE IkinciElKitapDB;

SELECT DATABASE();

CREATE TABLE Kullanicilar (
    KullaniciID INT AUTO_INCREMENT PRIMARY KEY,
    Ad VARCHAR(40),
    Soyad VARCHAR(40),
    Email VARCHAR(100) NOT NULL UNIQUE,
    Sifre VARCHAR(100) NOT NULL,
    Rol ENUM('Admin', 'Satici', 'Alici') NOT NULL
);

CREATE TABLE Kategoriler (
    KategoriID INT AUTO_INCREMENT PRIMARY KEY,
    KategoriAdi VARCHAR(50) NOT NULL
);

CREATE TABLE Kitaplar (
    KitapID INT AUTO_INCREMENT PRIMARY KEY,
    Baslik VARCHAR(100) NOT NULL,
    Yazar VARCHAR(60) NOT NULL,
    Yayinevi VARCHAR(50),
    Yil INT,
    Aciklama VARCHAR(150),
    KategoriID INT,
    FOREIGN KEY (KategoriID) REFERENCES Kategoriler(KategoriID)
        ON DELETE SET NULL
);

CREATE TABLE Ilanlar (
    IlanID INT AUTO_INCREMENT PRIMARY KEY,
    KitapID INT NOT NULL,
    SaticiID INT NOT NULL,
    Fiyat DECIMAL(10,2) NOT NULL,
    Durum ENUM('Yeni', 'IkinciEl') NOT NULL,
    ResimYolu VARCHAR(255),
    Tarih DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (KitapID) REFERENCES Kitaplar(KitapID)
        ON DELETE CASCADE,
    FOREIGN KEY (SaticiID) REFERENCES Kullanicilar(KullaniciID)
        ON DELETE CASCADE
);

CREATE TABLE Siparisler (
    SiparisID INT AUTO_INCREMENT PRIMARY KEY,
    IlanID INT NOT NULL,
    AliciID INT NOT NULL,
    SiparisTarihi DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (IlanID) REFERENCES Ilanlar(IlanID)
        ON DELETE CASCADE,
    FOREIGN KEY (AliciID) REFERENCES Kullanicilar(KullaniciID)
        ON DELETE CASCADE
);

CREATE TABLE Yorumlar (
    YorumID INT AUTO_INCREMENT PRIMARY KEY,
    KitapID INT NOT NULL,
    KullaniciID INT NOT NULL,
    Puan INT CHECK (Puan BETWEEN 1 AND 5),
    YorumMetni TEXT,
    Tarih DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (KitapID) REFERENCES Kitaplar(KitapID)
        ON DELETE CASCADE,
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID)
        ON DELETE CASCADE
);

CREATE TABLE Adresler(
    AdresID INT AUTO_INCREMENT PRIMARY KEY,
    KullaniciID INT NOT NULL,
    Baslik VARCHAR(50),
    Il VARCHAR(50),
    Ilce VARCHAR(50),
    AdresDetay TEXT,
    PostaKodu VARCHAR(10),
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID) ON DELETE CASCADE

);

CREATE TABLE Kartlar (
    KartID INT AUTO_INCREMENT PRIMARY KEY,
    KullaniciID INT NOT NULL,
    KartSahibiAdSoyad VARCHAR(100),
    KartNumarasi VARCHAR(20),
    SonKullanmaAy INT CHECK (SonKullanmaAy BETWEEN 1 AND 12),
    SonKullanmaYil INT CHECK (SonKullanmaYil BETWEEN 2024 AND 2100),
    CVC CHAR(3),
    KartTipi ENUM('Kredi', 'Banka') DEFAULT 'Kredi',
    FOREIGN KEY (KullaniciID) REFERENCES Kullanicilar(KullaniciID) ON DELETE CASCADE
);


INSERT INTO Kullanicilar (Ad, Soyad, Email, Sifre, Rol)
VALUES ('Ayşe',"Barım", 'ayse@example.com', '1234', 'Alici');


SELECT * FROM Kartlar;

SHOW CREATE TABLE Kullanicilar;
