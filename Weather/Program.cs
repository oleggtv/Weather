using System.Net;
using Newtonsoft.Json;
using Refit;
namespace Weather
{ 
    class Program {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Vvedi gorod");
            //string city = Console.ReadLine();
            //string city = "Cherkasy";
            string city = "Smila";
            string appid = "de120f7ac96fa836de3f0c35eed33709";
            string unit = "metric";
            string dirName = @"C:\myDir";
            //string path = @"C:\myDir\weather.txt";
            //string path = @"C:\myDir\weather1.txt";
            //FileInfo fileInfo = new FileInfo(path);
            //string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid=de120f7ac96fa836de3f0c35eed33709";
            string url = "http://api.openweathermap.org/data/2.5";

            DirectoryInfo dirInfo = new DirectoryInfo(dirName);
            
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            
            var api = RestService.For<IWeatherApi>(url);
            var jsonFilerepository = new JsonFileWeatherRepository();
            //var xmlWeatherRepository = new XmlFileWeatherRepository();
            WeatherService weatherService = new WeatherService(jsonFilerepository, api);
            //WeatherService weatherService = new WeatherService(xmlWeatherRepository, api);
            Weather weather = await GetWeatherAsync(weatherService, city, unit, appid, api);

            if (weather != null)
            {
                Console.WriteLine("Temperature in {0} {1} {2} C",
                weather.cityName, weather.dateTime, weather.weatherResponse.Main.Temp);
            }

        }
        
        static public async Task<Weather> GetWeatherAsync(WeatherService weatherService, string city,
            string unit, string appid, IWeatherApi api)
        {
            Weather weather = await weatherService.GetWeatherByCityNameAsync(city);
            Boolean isCity = false;
            if (weather != null && (DateTime.Now - weather.dateTime).Minutes < 1)
            {
                //Console.WriteLine((DateTime.Now - weather.dateTime).Minutes);
                return weather;
            }
            if (weather != null)
            {
                isCity = true;
            }
            var queryParams = new Dictionary<string, string>()
            {
                {"q", city},
                {"units", unit},
                {"appid", appid}
            };
            weather = new Weather(city, await api.GetWeatherAsync(queryParams), DateTime.Now);
            //Console.WriteLine(isCity);
            if (weather != null)
            {
                if (isCity)
                {
                    await weatherService.RemoveAsync(weather);
                    return weather;
                }
                await weatherService.AddAsync(weather);
                return weather;
            }
            Console.WriteLine("Problem, CityName Error");
            return weather;
        }
    }
}
