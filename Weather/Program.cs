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
            //string dirName = @"C:\myDir";
            //string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid=de120f7ac96fa836de3f0c35eed33709";
            string url = "http://api.openweathermap.org/data/2.5";

            //DirectoryInfo dirInfo = new DirectoryInfo(dirName);
            
            //if (!dirInfo.Exists)
            //{
            //    dirInfo.Create();
            //}
            
            var api = RestService.For<IWeatherApi>(url);
            //var jsonFilerepository = new JsonFileWeatherRepository();
            //var weatherService = new WeatherService(jsonFilerepository, api);
            var xmlWeatherRepository = new XmlFileWeatherRepository();
            var weatherService = new WeatherService(xmlWeatherRepository, api);
            //var weather = await GetWeather(weatherService, city, unit, appid, api);
            var weather = await weatherService.GetWeatherByCityNameAsync(city);

            if (weather != null)
            {
                Console.WriteLine("Temperature in {0} {1} {2} C",
                weather.cityName, weather.dateTime, weather.weatherResponse.Main.Temp);
            }
            else
            {
                Console.WriteLine("Problem, CityName Error");
            }

        }
        
        //static public async Task<Weather> GetWeather(WeatherService weatherService, string city,
        //    string unit, string appid, IWeatherApi api)
        //{
        //    var weather = weatherService.GetWeatherByCityNameAsync(city);
        //    if (weather != null && (DateTime.Now - weather.dateTime).Minutes < 1)
        //    {
        //        return weather;
        //    }
        //    if (weather != null)
        //    {
        //        await weatherService.RemoveAsync(weather);
        //    }
        //    var queryParams = new Dictionary<string, string>()
        //    {
        //        {"q", city},
        //        {"units", unit},
        //        {"appid", appid}
        //    };
        //    weather = new Weather(city, await api.GetWeatherAsync(queryParams), DateTime.Now);
        //    if (weather != null)
        //    {
        //        await weatherService.AddAsync(weather);
        //    }
        //    return weather;
        //}
    }
}
