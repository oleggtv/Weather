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
        public Task<Weather> GetWeatherByCityNameAsync(string cityName)
        {
            return _weatherRepository.GetWeatherByCityNameAsync(cityName);
        }
        public async Task AddAsync(Weather weather)
        {
            _weatherRepository.AddWeatherAsync(weather);
            
        }
        public async Task RemoveAsync(Weather weather)
        {
            _weatherRepository.RemoveWeatherAsync(weather);
        }
    }
}
