-- ============================================================================
-- CÃ‚U 1: CÆ  Sá»ž Dá»® LIá»†U QL_ChiTieu (á»¨NG Dá»¤NG Sá»” CHI TIÃŠU CÃ NHÃ‚N)
-- ============================================================================
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QL_ChiTieu')
BEGIN
    CREATE DATABASE QL_ChiTieu;
END
GO

USE QL_ChiTieu;
GO

IF OBJECT_ID(N'ChiTieu', N'U') IS NOT NULL
    DROP TABLE ChiTieu;
GO

CREATE TABLE ChiTieu (
    MaCT INT IDENTITY(1,1) PRIMARY KEY,
    NgayChi DATE NOT NULL,
    NoiDung NVARCHAR(100) NOT NULL,
    SoTien DECIMAL(18,0) NOT NULL,
    LoaiChi NVARCHAR(30) NOT NULL,
    GhiChu NVARCHAR(200) NULL
);
GO

-- Dá»¯ liá»‡u máº«u (Ä‚n uá»‘ng, Äi láº¡i, Há»c táº­p, Giáº£i trÃ­, KhÃ¡c)
INSERT INTO ChiTieu (NgayChi, NoiDung, SoTien, LoaiChi, GhiChu) VALUES
('2026-10-01', N'Ä‚n sÃ¡ng phá»Ÿ bÃ²', 45000, N'Ä‚n uá»‘ng', N'Phá»Ÿ bÃ² Cáº§u Giáº¥y'),
('2026-10-02', N'Äá»• xÄƒng xe mÃ¡y', 70000, N'Äi láº¡i', N'Äá»• Ä‘áº§y bÃ¬nh xÄƒng A95'),
('2026-10-03', N'Mua giÃ¡o trÃ¬nh C# nÃ¢ng cao', 120000, N'Há»c táº­p', N'SÃ¡ch thÆ° viá»‡n ÄHQG'),
('2026-10-05', N'Xem phim ráº¡p CGV', 110000, N'Giáº£i trÃ­', N'VÃ© phim cuá»‘i tuáº§n'),
('2026-10-07', N'Ä‚n trÆ°a cÆ¡m vÄƒn phÃ²ng', 40000, N'Ä‚n uá»‘ng', N'CÆ¡m táº¥m sÆ°á»n bÃ¬'),
('2026-10-09', N'Náº¡p tiá»n Ä‘iá»‡n thoáº¡i', 100000, N'KhÃ¡c', N'Tháº» Viettel 100k'),
('2026-10-10', N'Mua vá»Ÿ vÃ  bÃºt viáº¿t', 35000, N'Há»c táº­p', N'Vá»Ÿ Campus vÃ  bÃºt bi'),
('2026-10-12', N'Báº£o dÆ°á»¡ng thay nhá»›t xe', 150000, N'Äi láº¡i', N'Nhá»›t Castrol Power1'),
('2026-10-15', N'TrÃ  sá»¯a Ä‘á»“ng nghiá»‡p', 65000, N'Ä‚n uá»‘ng', N'TrÃ  sá»¯a trÃ¢n chÃ¢u Ä‘Æ°á»ng Ä‘en'),
('2026-10-16', N'ÄÄƒng kÃ½ khÃ³a há»c trá»±c tuyáº¿n', 299000, N'Há»c táº­p', N'KhÃ³a há»c EF Core'),
('2026-10-18', N'Äi bÆ¡i cuá»‘i tuáº§n', 80000, N'Giáº£i trÃ­', N'VÃ© bá»ƒ bÆ¡i 4 mÃ¹a'),
('2026-10-19', N'Mua cÃ  phÃª sÃ¡ng', 25000, N'Ä‚n uá»‘ng', N'CÃ  phÃª sá»¯a Ä‘Ã¡');
GO
-- ============================================================================
-- CÃ‚U 2: CÆ  Sá»ž Dá»® LIá»†U QL_NhaTro (á»¨NG Dá»¤NG QUáº¢N LÃ NHÃ€ TRá»Œ)
-- ============================================================================
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QL_NhaTro')
BEGIN
    CREATE DATABASE QL_NhaTro;
END
GO

USE QL_NhaTro;
GO

IF OBJECT_ID(N'KhachThue', N'U') IS NOT NULL
    DROP TABLE KhachThue;
GO

IF OBJECT_ID(N'PhongTro', N'U') IS NOT NULL
    DROP TABLE PhongTro;
GO

CREATE TABLE PhongTro (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(20) NOT NULL,
    GiaThue DECIMAL(18,0) NOT NULL,
    TinhTrang NVARCHAR(20) NOT NULL DEFAULT N'Trá»‘ng'
);
GO

CREATE TABLE KhachThue (
    MaKhach INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    GioiTinh NVARCHAR(5) NOT NULL,
    NgaySinh DATE NOT NULL,
    SoDienThoai VARCHAR(15) NOT NULL,
    MaTinh INT NOT NULL,
    TenTinh NVARCHAR(100) NOT NULL,
    MaPhuongXa INT NOT NULL,
    TenPhuongXa NVARCHAR(100) NOT NULL,
    DaDangKyTamTru BIT NOT NULL DEFAULT 0,
    MaPhong INT NOT NULL FOREIGN KEY REFERENCES PhongTro(MaPhong)
);
GO

-- 6 phÃ²ng trá» máº«u
INSERT INTO PhongTro (TenPhong, GiaThue, TinhTrang) VALUES
(N'P.101', 2500000, N'Äang thuÃª'),
(N'P.102', 2500000, N'Äang thuÃª'),
(N'P.103', 2800000, N'Trá»‘ng'),
(N'P.201', 3000000, N'Äang thuÃª'),
(N'P.202', 3000000, N'Trá»‘ng'),
(N'P.203', 3200000, N'Trá»‘ng');
GO

-- KhÃ¡ch thuÃª máº«u
INSERT INTO KhachThue (HoTen, GioiTinh, NgaySinh, SoDienThoai, MaTinh, TenTinh, MaPhuongXa, TenPhuongXa, DaDangKyTamTru, MaPhong) VALUES
(N'Nguyá»…n VÄƒn An', N'Nam', '2002-05-14', '0912345678', 1, N'ThÃ nh phá»‘ HÃ  Ná»™i', 611, N'PhÆ°á»ng XuÃ¢n Äá»‰nh', 1, 1),
(N'Tráº§n Thá»‹ BÃ­ch', N'Ná»¯', '2003-08-22', '0987654321', 1, N'ThÃ nh phá»‘ HÃ  Ná»™i', 643, N'PhÆ°á»ng Thanh Liá»‡t', 1, 2),
(N'LÃª HoÃ ng Long', N'Nam', '2001-11-30', '0905112233', 48, N'ThÃ nh phá»‘ ÄÃ  Náºµng', 20197, N'PhÆ°á»ng Tháº¡ch Thang', 0, 4);
GO
-- ============================================================================
-- CÃ‚U 3: CÆ  Sá»ž Dá»® LIá»†U QL_DaNgoai (á»¨NG Dá»¤NG Káº¾ HOáº CH DÃƒ NGOáº I)
-- ============================================================================
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QL_DaNgoai')
BEGIN
    CREATE DATABASE QL_DaNgoai;
END
GO

USE QL_DaNgoai;
GO

IF OBJECT_ID(N'ChuyenDi', N'U') IS NOT NULL
    DROP TABLE ChuyenDi;
GO

CREATE TABLE ChuyenDi (
    MaChuyen INT IDENTITY(1,1) PRIMARY KEY,
    TenChuyen NVARCHAR(100) NOT NULL,
    DiaDiem NVARCHAR(100) NOT NULL,
    NgayDi DATE NOT NULL,
    SoNguoi INT NOT NULL,
    NganSach DECIMAL(18,0) NOT NULL,
    PhuongTien NVARCHAR(30) NOT NULL,
    DaHoanThanh BIT NOT NULL DEFAULT 0,
    GhiChu NVARCHAR(200) NULL
);
GO

-- 8 chuyáº¿n Ä‘i máº«u
INSERT INTO ChuyenDi (TenChuyen, DiaDiem, NgayDi, SoNguoi, NganSach, PhuongTien, DaHoanThanh, GhiChu) VALUES
(N'KhÃ¡m phÃ¡ sÆ°Æ¡ng mÃ¹ ÄÃ  Láº¡t', N'ÄÃ  Láº¡t', '2026-10-25', 4, 8000000, N'MÃ¡y bay', 0, N'Mang theo Ã¡o áº¥m, chuáº©n bá»‹ mÃ¡y áº£nh'),
(N'SÄƒn mÃ¢y Fansipan Sa Pa', N'Sa Pa', '2026-10-28', 6, 12000000, N'TÃ u há»a', 0, N'LÃªn Ä‘á»‰nh Fansipan Ä‘i cÃ¡p treo'),
(N'Táº¯m biá»ƒn VÅ©ng TÃ u cuá»‘i tuáº§n', N'VÅ©ng TÃ u', '2026-10-22', 5, 5000000, N'Ã” tÃ´', 0, N'ThuÃª homestay gáº§n BÃ£i Sau'),
(N'Dáº¡o phá»‘ cá»• Há»™i An vá» Ä‘Ãªm', N'Há»™i An', '2026-11-01', 3, 9000000, N'MÃ¡y bay', 0, N'ThÆ°á»Ÿng thá»©c cao láº§u, tháº£ Ä‘Ã¨n hoa Ä‘Äƒng'),
(N'Cáº¯m tráº¡i há»“ Trá»‹ An', N'Äá»“ng Nai', '2026-10-20', 8, 4000000, N'Xe mÃ¡y', 0, N'Mang theo lá»u vÃ  báº¿p nÆ°á»›ng than'),
(N'Du ngoáº¡n Vá»‹nh Háº¡ Long', N'Háº¡ Long', '2026-10-15', 2, 7000000, N'Xe khÃ¡ch', 1, N'Tour du thuyá»n 2 ngÃ y 1 Ä‘Ãªm (Ä‘Ã£ Ä‘i)'),
(N'KhÃ¡m phÃ¡ Ä‘áº£o ngá»c PhÃº Quá»‘c', N'PhÃº Quá»‘c', '2026-11-03', 4, 16000000, N'MÃ¡y bay', 0, N'Láº·n ngáº¯m san hÃ´ táº¡i Nam Ä‘áº£o'),
(N'Trekking TÃ  NÄƒng Phan DÅ©ng', N'LÃ¢m Äá»“ng', '2026-11-10', 10, 15000000, N'Ã” tÃ´', 0, N'Cáº§n rÃ¨n luyá»‡n thá»ƒ lá»±c trÆ°á»›c 1 tuáº§n');
GO
-- ============================================================================
-- CÃ‚U 4: CÆ  Sá»ž Dá»® LIá»†U QL_TraSua (á»¨NG Dá»¤NG QUáº¢N LÃ BÃN HÃ€NG QUÃN TRÃ€ Sá»®A)
-- ============================================================================
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QL_TraSua')
BEGIN
    CREATE DATABASE QL_TraSua;
END
GO

USE QL_TraSua;
GO

IF OBJECT_ID(N'ChiTietHoaDon', N'U') IS NOT NULL
    DROP TABLE ChiTietHoaDon;
GO

IF OBJECT_ID(N'HoaDon', N'U') IS NOT NULL
    DROP TABLE HoaDon;
GO

IF OBJECT_ID(N'MonUong', N'U') IS NOT NULL
    DROP TABLE MonUong;
GO

IF OBJECT_ID(N'DanhMucMon', N'U') IS NOT NULL
    DROP TABLE DanhMucMon;
GO

CREATE TABLE DanhMucMon (
    MaDM INT IDENTITY(1,1) PRIMARY KEY,
    TenDM NVARCHAR(50) NOT NULL
);
GO

CREATE TABLE MonUong (
    MaMon INT IDENTITY(1,1) PRIMARY KEY,
    TenMon NVARCHAR(100) NOT NULL,
    GiaBan DECIMAL(18,0) NOT NULL,
    DangBan BIT NOT NULL DEFAULT 1,
    MaDM INT NOT NULL FOREIGN KEY REFERENCES DanhMucMon(MaDM)
);
GO

CREATE TABLE HoaDon (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    NgayLap DATETIME NOT NULL DEFAULT GETDATE(),
    TongTien DECIMAL(18,0) NOT NULL,
    HinhThucTT NVARCHAR(20) NOT NULL,
    DaThanhToan BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE ChiTietHoaDon (
    MaHD INT NOT NULL FOREIGN KEY REFERENCES HoaDon(MaHD) ON DELETE CASCADE,
    MaMon INT NOT NULL FOREIGN KEY REFERENCES MonUong(MaMon),
    SoLy INT NOT NULL,
    DonGia DECIMAL(18,0) NOT NULL,
    PRIMARY KEY (MaHD, MaMon)
);
GO

-- 4 danh má»¥c
INSERT INTO DanhMucMon (TenDM) VALUES
(N'TrÃ  sá»¯a truyá»n thá»‘ng'),
(N'TrÃ  trÃ¡i cÃ¢y nhiá»‡t Ä‘á»›i'),
(N'CÃ  phÃª & ÄÃ¡ xay'),
(N'Topping cao cáº¥p');
GO

-- 12+ mÃ³n uá»‘ng
INSERT INTO MonUong (TenMon, GiaBan, DangBan, MaDM) VALUES
(N'TrÃ  sá»¯a Oolong nÆ°á»›ng', 35000, 1, 1),
(N'TrÃ  sá»¯a TrÃ¢n chÃ¢u Ä‘en', 30000, 1, 1),
(N'TrÃ  sá»¯a Khoai mÃ´n dáº»o', 38000, 1, 1),
(N'TrÃ  sá»¯a Matcha Nháº­t Báº£n', 42000, 1, 1),
(N'TrÃ  Ä‘Ã o cam sáº£', 39000, 1, 2),
(N'TrÃ  dÃ¢u tÃ¢y á»•i há»“ng', 45000, 1, 2),
(N'TrÃ  hoa Ä‘áº­u biáº¿c chanh vÃ ng', 35000, 1, 2),
(N'CÃ  phÃª muá»‘i Huáº¿', 28000, 1, 3),
(N'Matcha Ä‘Ã¡ xay kem cheese', 49000, 1, 3),
(N'Caramel Macchiato Ä‘Ã¡ xay', 52000, 1, 3),
(N'TrÃ¢n chÃ¢u hoÃ ng kim', 10000, 1, 4),
(N'Tháº¡ch pudding trá»©ng', 10000, 1, 4),
(N'Kem phÃ´ mai Macchiato', 12000, 1, 4);
GO

-- HÃ³a Ä‘Æ¡n máº«u
INSERT INTO HoaDon (NgayLap, TongTien, HinhThucTT, DaThanhToan) VALUES
('2026-10-19 08:30:00', 74000, N'Tiá»n máº·t', 1),
('2026-10-19 09:15:00', 84000, N'Chuyá»ƒn khoáº£n', 1);
GO

INSERT INTO ChiTietHoaDon (MaHD, MaMon, SoLy, DonGia) VALUES
(1, 1, 2, 35000),
(1, 11, 1, 10000);
GO

INSERT INTO ChiTietHoaDon (MaHD, MaMon, SoLy, DonGia) VALUES
(2, 4, 2, 42000);
GO
-- ============================================================================
-- CÃ‚U 5: CÆ  Sá»ž Dá»® LIá»†U QL_DatTour (á»¨NG Dá»¤NG QUáº¢N LÃ Äáº¶T TOUR DU Lá»ŠCH)
-- ============================================================================
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QL_DatTour')
BEGIN
    CREATE DATABASE QL_DatTour;
END
GO

USE QL_DatTour;
GO

IF OBJECT_ID(N'DatTour', N'U') IS NOT NULL
    DROP TABLE DatTour;
GO

IF OBJECT_ID(N'KhachHang', N'U') IS NOT NULL
    DROP TABLE KhachHang;
GO

IF OBJECT_ID(N'Tour', N'U') IS NOT NULL
    DROP TABLE Tour;
GO

CREATE TABLE Tour (
    MaTour INT IDENTITY(1,1) PRIMARY KEY,
    TenTour NVARCHAR(100) NOT NULL,
    DiemDen NVARCHAR(50) NOT NULL,
    NgoaiTe CHAR(3) NOT NULL,
    GiaNgoaiTe DECIMAL(18,2) NOT NULL,
    NgayKhoiHanh DATE NOT NULL,
    SoChoToiDa INT NOT NULL
);
GO

CREATE TABLE KhachHang (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE DatTour (
    MaDat INT IDENTITY(1,1) PRIMARY KEY,
    MaKH INT NOT NULL FOREIGN KEY REFERENCES KhachHang(MaKH),
    MaTour INT NOT NULL FOREIGN KEY REFERENCES Tour(MaTour),
    SoNguoi INT NOT NULL,
    NgayDat DATETIME NOT NULL DEFAULT GETDATE(),
    TyGia DECIMAL(18,4) NOT NULL,
    TongTienVND DECIMAL(18,0) NOT NULL
);
GO

-- 5 tour máº«u
INSERT INTO Tour (TenTour, DiemDen, NgoaiTe, GiaNgoaiTe, NgayKhoiHanh, SoChoToiDa) VALUES
(N'KhÃ¡m phÃ¡ thiÃªn Ä‘Æ°á»ng Bangkok - Pattaya', N'ThÃ¡i Lan', 'THB', 12500.00, '2026-11-15', 25),
(N'MÃ¹a thu lÃ¡ Ä‘á» Seoul - Äáº£o Nami', N'HÃ n Quá»‘c', 'KRW', 850000.00, '2026-11-20', 20),
(N'Ngáº¯m nÃºi PhÃº SÄ© - Tráº£i nghiá»‡m Tokyo', N'Nháº­t Báº£n', 'JPY', 95000.00, '2026-12-05', 18),
(N'Du ngoáº¡n bá» TÃ¢y Hoa Ká»³ Los Angeles - Las Vegas', N'Má»¹', 'USD', 1800.00, '2026-12-20', 15),
(N'KhÃ¡m phÃ¡ Chiang Mai - Chiá»ng Rai cá»• kÃ­nh', N'ThÃ¡i Lan', 'THB', 11000.00, '2026-11-28', 20);
GO

-- KhÃ¡ch hÃ ng máº«u
INSERT INTO KhachHang (HoTen, SoDienThoai, Email) VALUES
(N'Pháº¡m Quang Huy', '0912999888', 'quanghuy@gmail.com'),
(N'VÅ© Mai PhÆ°Æ¡ng', '0988777666', 'maiphuong@outlook.com'),
(N'Äá»— Minh TrÃ­', '0903123456', 'minhtri@gmail.com');
GO

-- ÄÆ¡n Ä‘áº·t tour máº«u
INSERT INTO DatTour (MaKH, MaTour, SoNguoi, NgayDat, TyGia, TongTienVND) VALUES
(1, 1, 2, '2026-10-18 14:00:00', 772.0000, 19300000),
(2, 2, 1, '2026-10-19 09:30:00', 18.5000, 15725000);
GO
