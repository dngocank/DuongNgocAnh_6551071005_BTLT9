-- ============================================================================
-- CÂU 4: CƠ SỞ DỮ LIỆU QL_TraSua (ỨNG DỤNG QUẢN LÝ BÁN HÀNG QUÁN TRÀ SỮA)
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

-- 4 danh mục
INSERT INTO DanhMucMon (TenDM) VALUES
(N'Trà sữa truyền thống'),
(N'Trà trái cây nhiệt đới'),
(N'Cà phê & Đá xay'),
(N'Topping cao cấp');
GO

-- 12+ món uống
INSERT INTO MonUong (TenMon, GiaBan, DangBan, MaDM) VALUES
(N'Trà sữa Oolong nướng', 35000, 1, 1),
(N'Trà sữa Trân châu đen', 30000, 1, 1),
(N'Trà sữa Khoai môn dẻo', 38000, 1, 1),
(N'Trà sữa Matcha Nhật Bản', 42000, 1, 1),
(N'Trà đào cam sả', 39000, 1, 2),
(N'Trà dâu tây ổi hồng', 45000, 1, 2),
(N'Trà hoa đậu biếc chanh vàng', 35000, 1, 2),
(N'Cà phê muối Huế', 28000, 1, 3),
(N'Matcha đá xay kem cheese', 49000, 1, 3),
(N'Caramel Macchiato đá xay', 52000, 1, 3),
(N'Trân châu hoàng kim', 10000, 1, 4),
(N'Thạch pudding trứng', 10000, 1, 4),
(N'Kem phô mai Macchiato', 12000, 1, 4);
GO

-- Hóa đơn mẫu
INSERT INTO HoaDon (NgayLap, TongTien, HinhThucTT, DaThanhToan) VALUES
('2026-10-19 08:30:00', 74000, N'Tiền mặt', 1),
('2026-10-19 09:15:00', 84000, N'Chuyển khoản', 1);
GO

INSERT INTO ChiTietHoaDon (MaHD, MaMon, SoLy, DonGia) VALUES
(1, 1, 2, 35000),
(1, 11, 1, 10000);
GO

INSERT INTO ChiTietHoaDon (MaHD, MaMon, SoLy, DonGia) VALUES
(2, 4, 2, 42000);
GO
