using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Cau5_QuanLyDatTour.Config;

namespace Cau5_QuanLyDatTour.Services
{
    public class CachedExchangeRate
    {
        public decimal Rate { get; set; }
        public DateTime ExpirationTime { get; set; }
    }

    public static class TyGiaService
    {
        private static readonly HttpClient _httpClient;
        // In-memory cache lưu tỷ giá trong 10 phút
        private static readonly ConcurrentDictionary<string, CachedExchangeRate> _cache = new();

        static TyGiaService()
        {
            var config = AppConfig.Instance.ExchangeRate;
            int timeoutSec = config.TimeoutSeconds > 0 ? config.TimeoutSeconds : 8;
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(timeoutSec)
            };
        }

        public static async Task<decimal> LayTyGiaAsync(string ngoaiTe)
        {
            if (string.IsNullOrWhiteSpace(ngoaiTe))
                throw new ArgumentException("Mã ngoại tệ không hợp lệ", nameof(ngoaiTe));

            string code = ngoaiTe.Trim().ToLowerInvariant();

            // 1. Kiểm tra In-memory Cache (10 phút)
            if (_cache.TryGetValue(code, out var cachedItem))
            {
                if (DateTime.Now < cachedItem.ExpirationTime)
                {
                    LoggerService.LogInfo($"[Cache Hit] Lấy tỷ giá {ngoaiTe.ToUpper()} từ bộ nhớ đệm: {cachedItem.Rate:N4} VND (hết hạn lúc {cachedItem.ExpirationTime:HH:mm:ss})");
                    return cachedItem.Rate;
                }
            }

            var config = AppConfig.Instance.ExchangeRate;

            string primaryUrl = config.PrimaryUrl.Replace("{code}", code);
            string backupUrl = config.BackupUrl.Replace("{code}", code);

            decimal? tyGia = null;

            // 2. Thử gọi Endpoint chính
            try
            {
                string json = await _httpClient.GetStringAsync(primaryUrl);
                tyGia = ExtractVndRate(json, code);
                LoggerService.LogInfo($"[API Primary] Tỷ giá {ngoaiTe.ToUpper()} được phục vụ thành công từ endpoint chính: {primaryUrl}. Tỷ giá = {tyGia:N4} VND");
            }
            catch (Exception ex)
            {
                // Serilog ghi Warning khi địa chỉ chính lỗi kèm nguyên nhân
                LoggerService.LogWarning($"[API Fallback Triggered] Endpoint chính gặp sự cố: {primaryUrl}. Nguyên nhân: {ex.Message}. Đang chuyển hướng sang endpoint dự phòng...");
            }

            // 3. Nếu Endpoint chính lỗi -> Chuyển hướng sang Endpoint dự phòng
            if (tyGia == null)
            {
                try
                {
                    string jsonBackup = await _httpClient.GetStringAsync(backupUrl);
                    tyGia = ExtractVndRate(jsonBackup, code);
                    LoggerService.LogInfo($"[API Backup] Tỷ giá {ngoaiTe.ToUpper()} được phục vụ thành công từ endpoint dự phòng: {backupUrl}. Tỷ giá = {tyGia:N4} VND");
                }
                catch (Exception exBackup)
                {
                    LoggerService.LogError(exBackup, $"[API Failed] Cả endpoint chính và endpoint dự phòng đều thất bại khi lấy tỷ giá {ngoaiTe.ToUpper()}.");
                    throw new HttpRequestException($"Không thể lấy tỷ giá {ngoaiTe.ToUpper()} từ cả máy chủ chính lẫn dự phòng. Vui lòng kiểm tra lại kết nối mạng!", exBackup);
                }
            }

            // 4. Lưu kết quả vào Cache (10 phút)
            int cacheMinutes = config.CacheMinutes > 0 ? config.CacheMinutes : 10;
            _cache[code] = new CachedExchangeRate
            {
                Rate = tyGia.Value,
                ExpirationTime = DateTime.Now.AddMinutes(cacheMinutes)
            };

            return tyGia.Value;
        }

        private static decimal ExtractVndRate(string json, string code)
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (root.TryGetProperty(code, out var curObj) && curObj.TryGetProperty("vnd", out var vndProp))
            {
                return vndProp.GetDecimal();
            }
            throw new InvalidOperationException($"Không tìm thấy tỷ giá quy đổi sang 'vnd' trong dữ liệu trả về cho {code}.");
        }
    }
}
