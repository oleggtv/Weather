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
            Weather weatherGet = await _weatherRepository.GetWeatherByCityName(cityName);
            if (weatherGet != null && (DateTime.Now - weatherGet.dateTime).Minutes < 1)
            {
                return weatherGet;
            }
            var queryParams = new Dictionary<string, string>()
            {
                {"q", cityName},
                {"units", "metric"},
                {"appid", "de120f7ac96fa836de3f0c35eed33709"}
            };
            Weather weather = new Weather(cityName, await _weatherApi.GetWeatherAsync(queryParams), DateTime.Now);
            if (weather == null)
            {
                return null;
            }
            if (weatherGet == null)
            {
                await AddAsync(weather);
                return weather;
            }
            await ReplaceAsync(weather);
            return weather;
        }
        public async Task AddAsync(Weather weather)
        {
            _weatherRepository.AddWeatherAsync(weather);
            
        }
        public async Task RemoveAsync(Weather weather)
        {
            _weatherRepository.RemoveWeatherAsync(weather);
        }
        public async Task ReplaceAsync(Weather weather)
        {
            _weatherRepository.ReplaceWeatherAsync(weather);
        }
    }
}
