using System;
using System.IO;
using System.Text.Json;

namespace Cau3_KeHoachDaNgoai.Services
{
    public class UserSettings
    {
        public string Language { get; set; } = "vi"; // "vi" hoặc "en"
    }

    public static class UserSettingsService
    {
        private static string GetFilePath()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folder = Path.Combine(appData, "KeHoachDaNgoai");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return Path.Combine(folder, "usersettings.json");
        }

        public static UserSettings Load()
        {
            try
            {
                string path = GetFilePath();
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    return JsonSerializer.Deserialize<UserSettings>(json) ?? new UserSettings();
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogWarning("Không tải được usersettings.json: " + ex.Message);
            }
            return new UserSettings();
        }

        public static void Save(UserSettings settings)
        {
            try
            {
                string path = GetFilePath();
                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                LoggerService.LogWarning("Không lưu được usersettings.json: " + ex.Message);
            }
        }
    }
}
