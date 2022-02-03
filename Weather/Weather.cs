using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    [Serializable]
    public class Weather
    {
        public string cityName { get; set; }
        public WeatherResponse weatherResponse { get; set; }
        public DateTime dateTime { get; set; }

        public Weather()
        {

        }
        public Weather(string cityName, WeatherResponse weatherResponse, DateTime dateTime)
        {
            this.cityName = cityName;
            this.weatherResponse = weatherResponse;
            this.dateTime = dateTime;
        }
    }
}
