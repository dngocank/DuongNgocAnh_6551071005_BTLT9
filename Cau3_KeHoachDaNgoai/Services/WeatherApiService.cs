using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cau3_KeHoachDaNgoai.Services
{
    public class WeatherForecastResult
    {
        public bool Success { get; set; }
        public bool Exceeds16Days { get; set; }
        public string Message { get; set; } = string.Empty;
        public int WeatherCode { get; set; }
        public string WeatherDescriptionVi { get; set; } = string.Empty;
        public string WeatherDescriptionEn { get; set; } = string.Empty;
        public string WeatherIcon { get; set; } = "🌤️";
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int RainProbability { get; set; }
        public bool IsRainWarning => RainProbability >= 60;
    }

    public class GeocodingResponse
    {
        public List<GeocodingItem>? results { get; set; }
    }

    public class GeocodingItem
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string name { get; set; } = string.Empty;
    }

    public class OpenMeteoResponse
    {
        public OpenMeteoDaily? daily { get; set; }
    }

    public class OpenMeteoDaily
    {
        public List<string>? time { get; set; }
        public List<int>? weather_code { get; set; }
        public List<double>? temperature_2m_max { get; set; }
        public List<double>? temperature_2m_min { get; set; }
        public List<int>? precipitation_probability_max { get; set; }
    }

    public static class WeatherApiService
    {
        // 1 HttpClient dùng chung, Timeout = 8 giây theo yêu cầu
        private static readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(8)
        };

        public static async Task<WeatherForecastResult> GetForecastAsync(string diaDiem, DateTime ngayDi)
        {
            var result = new WeatherForecastResult();

            int daysDiff = (ngayDi.Date - DateTime.Today).Days;
            if (daysDiff < 0 || daysDiff > 15)
            {
                result.Success = false;
                result.Exceeds16Days = true;
                result.Message = daysDiff < 0 
                    ? "Ngày đi đã qua, không có dự báo tương lai."
                    : "Chưa có dữ liệu dự báo (vượt quá 16 ngày).";
                return result;
            }

            try
            {
                // Bước 1: Geocoding
                string geoUrl = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(diaDiem)}&count=1&language=vi";
                string geoJson = await _httpClient.GetStringAsync(geoUrl);
                var geoData = JsonSerializer.Deserialize<GeocodingResponse>(geoJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                double lat = 0, lon = 0;
                if (geoData?.results != null && geoData.results.Count > 0)
                {
                    lat = geoData.results[0].latitude;
                    lon = geoData.results[0].longitude;
                }
                else
                {
                    // Thử lại với tên không dấu
                    string unsigned = RemoveDiacritics(diaDiem);
                    if (!string.Equals(unsigned, diaDiem, StringComparison.OrdinalIgnoreCase))
                    {
                        string fallbackGeoUrl = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(unsigned)}&count=1&language=vi";
                        string fallbackJson = await _httpClient.GetStringAsync(fallbackGeoUrl);
                        var fallbackData = JsonSerializer.Deserialize<GeocodingResponse>(fallbackJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (fallbackData?.results != null && fallbackData.results.Count > 0)
                        {
                            lat = fallbackData.results[0].latitude;
                            lon = fallbackData.results[0].longitude;
                        }
                    }
                }

                if (lat == 0 && lon == 0)
                {
                    result.Success = false;
                    result.Message = $"Không tìm thấy tọa độ địa danh '{diaDiem}'. Hãy thử nhập tên không dấu (ví dụ: 'Da Lat', 'Vung Tau').";
                    return result;
                }

                // Bước 2: Forecast
                string forecastUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat.ToString(CultureInfo.InvariantCulture)}&longitude={lon.ToString(CultureInfo.InvariantCulture)}&daily=weather_code,temperature_2m_max,temperature_2m_min,precipitation_probability_max&timezone=auto&forecast_days=16";
                string forecastJson = await _httpClient.GetStringAsync(forecastUrl);
                var forecast = JsonSerializer.Deserialize<OpenMeteoResponse>(forecastJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (forecast?.daily?.time == null)
                {
                    result.Success = false;
                    result.Message = "Không có dữ liệu thời tiết phản hồi từ Open-Meteo.";
                    return result;
                }

                string targetDateStr = ngayDi.ToString("yyyy-MM-dd");
                int index = forecast.daily.time.IndexOf(targetDateStr);
                if (index == -1)
                {
                    result.Success = false;
                    result.Exceeds16Days = true;
                    result.Message = "Chưa có dữ liệu dự báo cho ngày này.";
                    return result;
                }

                int code = forecast.daily.weather_code != null && forecast.daily.weather_code.Count > index
                    ? forecast.daily.weather_code[index] : 0;
                double maxT = forecast.daily.temperature_2m_max != null && forecast.daily.temperature_2m_max.Count > index
                    ? forecast.daily.temperature_2m_max[index] : 0;
                double minT = forecast.daily.temperature_2m_min != null && forecast.daily.temperature_2m_min.Count > index
                    ? forecast.daily.temperature_2m_min[index] : 0;
                int rainProb = forecast.daily.precipitation_probability_max != null && forecast.daily.precipitation_probability_max.Count > index
                    ? forecast.daily.precipitation_probability_max[index] : 0;

                result.Success = true;
                result.WeatherCode = code;
                result.TempMax = maxT;
                result.TempMin = minT;
                result.RainProbability = rainProb;

                // Quy ước mô tả & biểu tượng theo weather_code đề bài
                ParseWeatherCode(code, result);

                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Lỗi khi gọi API thời tiết: " + ex.Message;
                return result;
            }
        }

        private static void ParseWeatherCode(int code, WeatherForecastResult result)
        {
            if (code == 0)
            {
                result.WeatherDescriptionVi = "Trời quang đãng";
                result.WeatherDescriptionEn = "Clear sky";
                result.WeatherIcon = "☀️";
            }
            else if (code >= 1 && code <= 3)
            {
                result.WeatherDescriptionVi = "Có mây";
                result.WeatherDescriptionEn = "Partly cloudy";
                result.WeatherIcon = "⛅";
            }
            else if (code == 45 || code == 48)
            {
                result.WeatherDescriptionVi = "Sương mù";
                result.WeatherDescriptionEn = "Foggy";
                result.WeatherIcon = "🌫️";
            }
            else if ((code >= 51 && code <= 67) || (code >= 80 && code <= 82))
            {
                result.WeatherDescriptionVi = "Có mưa";
                result.WeatherDescriptionEn = "Rainy";
                result.WeatherIcon = "🌧️";
            }
            else if (code >= 95 && code <= 99)
            {
                result.WeatherDescriptionVi = "Có dông";
                result.WeatherDescriptionEn = "Thunderstorm";
                result.WeatherIcon = "⛈️";
            }
            else
            {
                result.WeatherDescriptionVi = "Bình thường";
                result.WeatherDescriptionEn = "Moderate";
                result.WeatherIcon = "🌤️";
            }
        }

        private static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            string result = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
            return result.Replace("đ", "d").Replace("Đ", "D");
        }
    }
}
