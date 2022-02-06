using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Weather
{
    public class JsonFileWeatherRepository: IWeatherRepository
    {
        FileInfo fileinfo = new FileInfo(@"C:\myDir\weather.json");
        Weather IWeatherRepository.GetWeatherByCityName(string cityName)
        {
            if (!fileinfo.Exists || fileinfo.Length == 0)
            {
                return null;
            }
            var weatherJson = File.ReadAllText(fileinfo.FullName);
            var weathers = JsonSerializer.Deserialize<List<Weather>>(weatherJson);
            return weathers?.FirstOrDefault(w => w.cityName.Equals(cityName));
        }

        async Task IWeatherRepository.AddWeatherAsync(Weather weather)
        {
            var weathers = new List<Weather>();
            if (fileinfo.Exists)
            {
                var weatherJson = File.ReadAllText(fileinfo.FullName);
                weathers = JsonSerializer.Deserialize<List<Weather>>(weatherJson);
            }
            weathers.Add(weather);
            File.WriteAllText(fileinfo.FullName, JsonSerializer.Serialize(weathers));
        }

        async Task IWeatherRepository.RemoveWeatherAsync(Weather weather)
        {
            var weatherJson = File.ReadAllText(fileinfo.FullName);
            var weathers = JsonSerializer.Deserialize<List<Weather>>(weatherJson);
            foreach (Weather w in weathers)
            {
                if (w.cityName.Equals(weather.cityName))
                {
                    weathers.Remove(w);
                    break;
                }
            }
            File.WriteAllText(fileinfo.FullName, JsonSerializer.Serialize(weathers));
        }
    }
}
