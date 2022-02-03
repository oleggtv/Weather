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
        async Task<Weather> IWeatherRepository.GetWeatherByCityNameAsync(string cityName)
        {
            Console.WriteLine("get");
            FileInfo file = new FileInfo(@"C:\myDir\weather.txt");
            if (!file.Exists)
            {
                Console.WriteLine("no file");
                return null;
            }
            using (StreamReader sr = new StreamReader(@"C:\myDir\weather.txt", System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    Weather weather = JsonSerializer.Deserialize<Weather>(line);
                    if (weather.cityName == cityName)
                    {
                        return weather;
                    }
                }
            }
            return null;
        }

        async Task IWeatherRepository.AddWeatherAsync(Weather weather)
        {
            Console.WriteLine("add");
            string json = JsonSerializer.Serialize(weather);
            FileInfo file = new FileInfo(@"C:\myDir\weather.txt");
            if (!file.Exists)
            {
                file.Create();
            }
            using (StreamWriter sw = new StreamWriter(@"C:\myDir\weather.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine(json);
            }
            //Console.WriteLine(json);
        }

        async Task IWeatherRepository.RemoveWeatherAsync(Weather weather)
        {
            Console.WriteLine("remove");
            string json = JsonSerializer.Serialize(weather);
            List<string> list = new List<string>();
            using (StreamReader sr = new StreamReader(@"C:\myDir\weather.txt", System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (JsonSerializer.Deserialize<Weather>(line).cityName == JsonSerializer.Deserialize<Weather>(json).cityName)
                    {
                        list.Add(json);
                    }
                    else
                    {
                        list.Add(line);
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(@"C:\myDir\weather.txt", false, System.Text.Encoding.Default))
            {
                foreach (string line in list)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
