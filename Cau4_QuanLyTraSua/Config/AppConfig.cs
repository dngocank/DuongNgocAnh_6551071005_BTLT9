using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Cau4_QuanLyTraSua.Config
{
    public class ConnectionStringsConfig
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }

    public class PaymentConfig
    {
        public string BankId { get; set; } = "ICB";
        public string AccountNo { get; set; } = "123456789";
        public string AccountName { get; set; } = "NGUYEN VAN A";
        public string Template { get; set; } = "compact2";
        public string StoreName { get; set; } = "TIEM TRA SUA ABC";
    }

    public class AppConfig
    {
        public ConnectionStringsConfig ConnectionStrings { get; set; } = new();
        public PaymentConfig Payment { get; set; } = new();

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
                    ?? "Server=(localdb)\\mssqllocaldb;Database=QL_TraSua;Trusted_Connection=True;TrustServerCertificate=True";
            }

            return config;
        }
    }
}
