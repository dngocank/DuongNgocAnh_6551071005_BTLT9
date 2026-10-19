using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Cau5_QuanLyDatTour.Config
{
    public class ConnectionStringsConfig
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }

    public class ExchangeRateConfig
    {
        public string PrimaryUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@latest/v1/currencies/{code}.json";
        public string BackupUrl { get; set; } = "https://latest.currency-api.pages.dev/v1/currencies/{code}.json";
        public int TimeoutSeconds { get; set; } = 8;
        public int CacheMinutes { get; set; } = 10;
    }

    public class AppConfig
    {
        public ConnectionStringsConfig ConnectionStrings { get; set; } = new();
        public ExchangeRateConfig ExchangeRate { get; set; } = new();

        private static AppConfig? _instance;
        public static AppConfig Instance => _instance ??= Load();

        public static AppConfig Load()
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            var config = new AppConfig();
            configuration.Bind(config);

            if (string.IsNullOrWhiteSpace(config.ConnectionStrings.DefaultConnection))
            {
                config.ConnectionStrings.DefaultConnection = configuration.GetConnectionString("DefaultConnection") 
                    ?? "Server=(localdb)\\mssqllocaldb;Database=QL_DatTour;Trusted_Connection=True;TrustServerCertificate=True";
            }

            return config;
        }
    }
}
