using System;
using System.Threading;
using System.Windows.Forms;
using Cau5_QuanLyDatTour.Services;

namespace Cau5_QuanLyDatTour
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            LoggerService.Initialize();

            // Đăng ký bắt lỗi toàn cục (Global Exception Handling) theo mục 9.3
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += (sender, e) =>
            {
                HandleGlobalException(e.Exception, "UI ThreadException");
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    HandleGlobalException(ex, "AppDomain UnhandledException");
                }
            };

            try
            {
                Application.Run(new FormDatTour());
            }
            finally
            {
                LoggerService.CloseAndFlush();
            }
        }

        private static void HandleGlobalException(Exception ex, string source)
        {
            LoggerService.LogError(ex, $"[GlobalException] Lỗi toàn cục chưa được xử lý từ {source}: {ex.Message}");
            MessageBox.Show("Đã xảy ra lỗi, thông tin đã được ghi lại vào file log!",
                "Thông báo sự cố", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
