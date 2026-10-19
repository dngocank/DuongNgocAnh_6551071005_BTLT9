-- ============================================================================
-- CÂU 3: CƠ SỞ DỮ LIỆU QL_DaNgoai (ỨNG DỤNG KẾ HOẠCH DÃ NGOẠI)
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

-- 8 chuyến đi mẫu
INSERT INTO ChuyenDi (TenChuyen, DiaDiem, NgayDi, SoNguoi, NganSach, PhuongTien, DaHoanThanh, GhiChu) VALUES
(N'Khám phá sương mù Đà Lạt', N'Đà Lạt', '2026-10-25', 4, 8000000, N'Máy bay', 0, N'Mang theo áo ấm, chuẩn bị máy ảnh'),
(N'Săn mây Fansipan Sa Pa', N'Sa Pa', '2026-10-28', 6, 12000000, N'Tàu hỏa', 0, N'Lên đỉnh Fansipan đi cáp treo'),
(N'Tắm biển Vũng Tàu cuối tuần', N'Vũng Tàu', '2026-10-22', 5, 5000000, N'Ô tô', 0, N'Thuê homestay gần Bãi Sau'),
(N'Dạo phố cổ Hội An về đêm', N'Hội An', '2026-11-01', 3, 9000000, N'Máy bay', 0, N'Thưởng thức cao lầu, thả đèn hoa đăng'),
(N'Cắm trại hồ Trị An', N'Đồng Nai', '2026-10-20', 8, 4000000, N'Xe máy', 0, N'Mang theo lều và bếp nướng than'),
(N'Du ngoạn Vịnh Hạ Long', N'Hạ Long', '2026-10-15', 2, 7000000, N'Xe khách', 1, N'Tour du thuyền 2 ngày 1 đêm (đã đi)'),
(N'Khám phá đảo ngọc Phú Quốc', N'Phú Quốc', '2026-11-03', 4, 16000000, N'Máy bay', 0, N'Lặn ngắm san hô tại Nam đảo'),
(N'Trekking Tà Năng Phan Dũng', N'Lâm Đồng', '2026-11-10', 10, 15000000, N'Ô tô', 0, N'Cần rèn luyện thể lực trước 1 tuần');
GO
