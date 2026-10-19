-- ============================================================================
-- CÂU 1: CƠ SỞ DỮ LIỆU QL_ChiTieu (ỨNG DỤNG SỔ CHI TIÊU CÁ NHÂN)
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

-- Dữ liệu mẫu (Ăn uống, Đi lại, Học tập, Giải trí, Khác)
INSERT INTO ChiTieu (NgayChi, NoiDung, SoTien, LoaiChi, GhiChu) VALUES
('2026-10-01', N'Ăn sáng phở bò', 45000, N'Ăn uống', N'Phở bò Cầu Giấy'),
('2026-10-02', N'Đổ xăng xe máy', 70000, N'Đi lại', N'Đổ đầy bình xăng A95'),
('2026-10-03', N'Mua giáo trình C# nâng cao', 120000, N'Học tập', N'Sách thư viện ĐHQG'),
('2026-10-05', N'Xem phim rạp CGV', 110000, N'Giải trí', N'Vé phim cuối tuần'),
('2026-10-07', N'Ăn trưa cơm văn phòng', 40000, N'Ăn uống', N'Cơm tấm sườn bì'),
('2026-10-09', N'Nạp tiền điện thoại', 100000, N'Khác', N'Thẻ Viettel 100k'),
('2026-10-10', N'Mua vở và bút viết', 35000, N'Học tập', N'Vở Campus và bút bi'),
('2026-10-12', N'Bảo dưỡng thay nhớt xe', 150000, N'Đi lại', N'Nhớt Castrol Power1'),
('2026-10-15', N'Trà sữa đồng nghiệp', 65000, N'Ăn uống', N'Trà sữa trân châu đường đen'),
('2026-10-16', N'Đăng ký khóa học trực tuyến', 299000, N'Học tập', N'Khóa học EF Core'),
('2026-10-18', N'Đi bơi cuối tuần', 80000, N'Giải trí', N'Vé bể bơi 4 mùa'),
('2026-10-19', N'Mua cà phê sáng', 25000, N'Ăn uống', N'Cà phê sữa đá');
GO
