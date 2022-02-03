using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Weather
{
    public class XmlFileWeatherRepository : IWeatherRepository
    {
        async Task<Weather> IWeatherRepository.GetWeatherByCityNameAsync(string cityName)
        {
            Console.WriteLine("get xml");
            FileInfo fileInfo = new FileInfo(@"C:\myDir\weather.xml");
            if (!fileInfo.Exists)
            {
                Console.WriteLine("no file");
                return null;
            }
            XmlSerializer xml = new XmlSerializer(typeof(List<Weather>));
            using (FileStream fs = new FileStream(@"C:\myDir\weather.xml", FileMode.OpenOrCreate))
            {
                List<Weather> weatherList = xml.Deserialize(fs) as List<Weather>;
                foreach (Weather weather in weatherList)
                {
                    if (weather.cityName == cityName)
                    {
                        Console.WriteLine(cityName);
                        return weather;
                    }
                }
            }
            return null;
        }

        async Task IWeatherRepository.AddWeatherAsync(Weather weather)
        {
            Console.WriteLine("add xml");
            List<Weather> weatherList = new List<Weather>();
            XmlSerializer xml = new XmlSerializer(typeof(List<Weather>));
            FileInfo fileInfo = new FileInfo(@"C:\myDir\weather.xml");
            if (!fileInfo.Exists)
            {
                Console.WriteLine("add no file");
            }
            else
            {
                Console.WriteLine("add yes file");
                using (FileStream fs = new FileStream(@"C:\myDir\weather.xml", FileMode.Open))
                {
                    weatherList = (List<Weather>)xml.Deserialize(fs);
                }
            }
            Console.WriteLine("add to xml");
            weatherList.Add(weather);
            Console.WriteLine(weatherList);
            using (FileStream fs = new FileStream(@"C:\myDir\weather.xml", FileMode.Create))
            {
                xml.Serialize(fs, weatherList);
            }
        }

        async Task IWeatherRepository.RemoveWeatherAsync(Weather weather)
        {
            Console.WriteLine("remove xml");
            List<Weather> weatherList = new List<Weather>();
            XmlSerializer xml = new XmlSerializer(typeof(List<Weather>));
            using (FileStream fs = new FileStream(@"C:\myDir\weather.xml", FileMode.Open))
            {
                weatherList = (List<Weather>)xml.Deserialize(fs);
            }
            Weather wRemove = new Weather();
            foreach (Weather w in weatherList)
            {
                if (w.cityName == weather.cityName)
                {
                    wRemove = w;
                    break;
                }
            }
            weatherList.Remove(wRemove);
            weatherList.Add(weather);
            Console.WriteLine(weather.cityName);
            Console.WriteLine(weather.dateTime);
            using (FileStream fs = new FileStream(@"C:\myDir\weather.xml", FileMode.Create))
            {
                xml.Serialize(fs, weatherList);
            }
        }
    }
}
