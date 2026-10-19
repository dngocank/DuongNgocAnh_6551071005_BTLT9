using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Cau1_SoChiTieu.Config
{
    public class ConnectionStringsConfig
    {
        public string DefaultConnection { get; set; } = string.Empty;
    }

    public class AppConfig
    {
        public ConnectionStringsConfig ConnectionStrings { get; set; } = new();

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
                    ?? "Server=(localdb)\\mssqllocaldb;Database=QL_ChiTieu;Trusted_Connection=True;TrustServerCertificate=True";
            }

            return config;
        }
    }
}
