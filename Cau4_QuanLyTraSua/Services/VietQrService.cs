using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Cau4_QuanLyTraSua.Config;

namespace Cau4_QuanLyTraSua.Services
{
    public static class VietQrService
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10)
        };

        public static string BuildVietQrUrl(decimal amount, string addInfo)
        {
            var config = AppConfig.Instance.Payment;
            string bankId = Uri.EscapeDataString(config.BankId);
            string accountNo = Uri.EscapeDataString(config.AccountNo);
            string template = Uri.EscapeDataString(config.Template);
            string accountName = Uri.EscapeDataString(config.AccountName);
            string info = Uri.EscapeDataString(addInfo);
            string amountStr = ((long)amount).ToString();

            return $"https://img.vietqr.io/image/{bankId}-{accountNo}-{template}.png?amount={amountStr}&addInfo={info}&accountName={accountName}";
        }

        public static async Task<Image?> FetchQrImageAsync(string qrUrl, string orderInfo, decimal amount)
        {
            try
            {
                byte[] bytes = await _httpClient.GetByteArrayAsync(qrUrl);
                using var ms = new MemoryStream(bytes);
                // Tạo một bản sao ảnh độc lập để không khóa Stream
                Image image = Image.FromStream(ms);
                var bitmap = new Bitmap(image);

                // Serilog: bảo mật số tài khoản, chỉ ghi mã đơn/thông tin và số tiền
                LoggerService.LogInfo($"Tạo mã VietQR thành công: Thông tin={orderInfo}, Số tiền={amount:N0} đ");
                return bitmap;
            }
            catch (Exception ex)
            {
                LoggerService.LogError(ex, $"Nạp ảnh VietQR thất bại: {qrUrl}");
                return null;
            }
        }
    }
}
