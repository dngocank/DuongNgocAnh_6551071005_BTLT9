using System;
using System.IO;
using Serilog;

namespace Cau3_KeHoachDaNgoai.Services
{
    public static class LoggerService
    {
        private static bool _initialized = false;

        public static void Initialize()
        {
            if (_initialized) return;

            string logDir = Path.Combine(AppContext.BaseDirectory, "Logs");
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            string logPath = Path.Combine(logDir, "log-.txt");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    path: logPath,
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            _initialized = true;
            Log.Information("Ứng dụng Kế hoạch dã ngoại khởi động.");
        }

        public static void LogInfo(string message) => Log.Information(message);
        public static void LogWarning(string message) => Log.Warning(message);
        public static void LogError(Exception ex, string message) => Log.Error(ex, message);
        public static void CloseAndFlush() => Log.CloseAndFlush();
    }
}
