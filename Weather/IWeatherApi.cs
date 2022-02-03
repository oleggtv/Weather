using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace Weather
{
    public interface IWeatherApi
    {
        [Get("/weather")]
        Task<WeatherResponse> GetWeatherAsync(Dictionary<string, string> query);
    }
}
