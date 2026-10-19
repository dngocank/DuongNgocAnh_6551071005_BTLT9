# BÀI TẬP CHƯƠNG 11: PUBLISH APP & TÍCH HỢP API (C# WINFORMS - .NET 8)

Dự án tổng hợp 5 bài tập Lập trình Trực quan (C#) về **Publish App, Cấu hình đa môi trường, Ghi log Serilog, Xử lý ngoại lệ toàn cục và Tích hợp RESTful API**.

---

## DANH SÁCH 5 BÀI TẬP

| Dự án | Tên bài tập | CSDL | Công nghệ & API chính |
| :--- | :--- | :--- | :--- |
| **Cau1_SoChiTieu** | Sổ chi tiêu cá nhân | `QL_ChiTieu` | EF Core Database First, AppSettings strongly-typed, Serilog, Publish Framework-Dependent |
| **Cau2_QuanLyNhaTro** | Quản lý nhà trọ | `QL_NhaTro` | API Tỉnh/Xã (Open-API v2), Đồng bộ trạng thái phòng, Bắt lỗi toàn cục (Global Exception) |
| **Cau3_KeHoachDaNgoai** | Kế hoạch dã ngoại | `QL_DaNgoai` | Open-Meteo Geocoding & Forecast API, Cảnh báo thời tiết xấu, Song ngữ Việt - Anh, `%LocalAppData%` |
| **Cau4_QuanLyTraSua** | Bán hàng quán trà sữa | `QL_TraSua` | VietQR API động, PaymentConfig, Giao dịch toàn vẹn SaveChanges, Báo cáo doanh thu ngày |
| **Cau5_QuanLyDatTour** | Quản lý đặt tour du lịch | `QL_DatTour` | API Tỷ giá đa nguồn (Primary & Backup Fallback), In-memory Cache 10 phút, Publish Single-File Self-Contained |

---

## HƯỚNG DẪN KHỞI TẠO CƠ SỞ DỮ LIỆU

### Cách 1: Khởi tạo tất cả 5 CSDL cùng lúc (Khuyên dùng)
Chạy lệnh PowerShell / Command Prompt tại thư mục gốc của repository:
```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -f 65001 -i "script.sql"
```
*(Hoặc mở file `script.sql` trong SQL Server Management Studio (SSMS) và nhấn Execute).*

### Cách 2: Khởi tạo riêng từng bài
Mỗi thư mục project đều có sẵn file `script.sql` riêng:
- `Cau1_SoChiTieu/script.sql` $\rightarrow$ CSDL `QL_ChiTieu`
- `Cau2_QuanLyNhaTro/script.sql` $\rightarrow$ CSDL `QL_NhaTro`
- `Cau3_KeHoachDaNgoai/script.sql` $\rightarrow$ CSDL `QL_DaNgoai`
- `Cau4_QuanLyTraSua/script.sql` $\rightarrow$ CSDL `QL_TraSua`
- `Cau5_QuanLyDatTour/script.sql` $\rightarrow$ CSDL `QL_DatTour`

---

## HƯỚNG DẪN CHẠY BẰNG VISUAL STUDIO

1. Mở file `BTapPublish_App_API.sln` bằng Visual Studio 2022.
2. Trong cửa sổ **Solution Explorer**, click chuột phải vào bài tập muốn chạy $\rightarrow$ chọn **Set as Startup Project**.
3. Nhấn **F5** (hoặc **Ctrl + F5**) để khởi chạy.
