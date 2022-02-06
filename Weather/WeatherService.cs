using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    public class WeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IWeatherApi _weatherApi;
        public WeatherService(IWeatherRepository weatherRepository, IWeatherApi weatherApi)
        {
            _weatherRepository = weatherRepository;
            _weatherApi = weatherApi;
        }
        public async Task<Weather> GetWeatherByCityNameAsync(string cityName)
        {
            Weather weather = await _weatherRepository.GetWeatherByCityName(cityName);
            Weather w = await GetWeather(weather);
            return w;
        }
        public async Task AddAsync(Weather weather)
        {
            _weatherRepository.AddWeatherAsync(weather);
            
        }
        public async Task RemoveAsync(Weather weather)
        {
            _weatherRepository.RemoveWeatherAsync(weather);
        }

        public async Task<Weather> GetWeather(Weather weather)
        {
            if (weather != null && (DateTime.Now - weather.dateTime).Minutes < 1)
            {
                return weather;
            }
            if (weather != null)
            {
                await RemoveAsync(weather);
            }
            var queryParams = new Dictionary<string, string>()
            {
                {"q", weather.cityName},
                {"units", "metric"},
                {"appid", "de120f7ac96fa836de3f0c35eed33709"}
            };
            weather = new Weather(weather.cityName, await _weatherApi.GetWeatherAsync(queryParams), DateTime.Now);
            if (weather != null)
            {
                await AddAsync(weather);
            }
            return weather;
        }
    }
}
