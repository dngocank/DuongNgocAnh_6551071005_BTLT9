-- ============================================================================
-- CÂU 5: CƠ SỞ DỮ LIỆU QL_DatTour (ỨNG DỤNG QUẢN LÝ ĐẶT TOUR DU LỊCH)
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

-- 5 tour mẫu
INSERT INTO Tour (TenTour, DiemDen, NgoaiTe, GiaNgoaiTe, NgayKhoiHanh, SoChoToiDa) VALUES
(N'Khám phá thiên đường Bangkok - Pattaya', N'Thái Lan', 'THB', 12500.00, '2026-11-15', 25),
(N'Mùa thu lá đỏ Seoul - Đảo Nami', N'Hàn Quốc', 'KRW', 850000.00, '2026-11-20', 20),
(N'Ngắm núi Phú Sĩ - Trải nghiệm Tokyo', N'Nhật Bản', 'JPY', 95000.00, '2026-12-05', 18),
(N'Du ngoạn bờ Tây Hoa Kỳ Los Angeles - Las Vegas', N'Mỹ', 'USD', 1800.00, '2026-12-20', 15),
(N'Khám phá Chiang Mai - Chiềng Rai cổ kính', N'Thái Lan', 'THB', 11000.00, '2026-11-28', 20);
GO

-- Khách hàng mẫu
INSERT INTO KhachHang (HoTen, SoDienThoai, Email) VALUES
(N'Phạm Quang Huy', '0912999888', 'quanghuy@gmail.com'),
(N'Vũ Mai Phương', '0988777666', 'maiphuong@outlook.com'),
(N'Đỗ Minh Trí', '0903123456', 'minhtri@gmail.com');
GO

-- Đơn đặt tour mẫu
INSERT INTO DatTour (MaKH, MaTour, SoNguoi, NgayDat, TyGia, TongTienVND) VALUES
(1, 1, 2, '2026-10-18 14:00:00', 772.0000, 19300000),
(2, 2, 1, '2026-10-19 09:30:00', 18.5000, 15725000);
GO
