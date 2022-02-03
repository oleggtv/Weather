using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    public interface IWeatherRepository
    {
        Task<Weather> GetWeatherByCityNameAsync(string cityName);
        Task AddWeatherAsync(Weather weather);
        Task RemoveWeatherAsync(Weather weather);
    }
}
