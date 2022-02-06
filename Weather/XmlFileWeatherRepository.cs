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
        FileInfo fileInfo = new FileInfo(@"C:\myDir\weather.xml");
        async Task<Weather> IWeatherRepository.GetWeatherByCityName(string cityName)
        {
            if (!fileInfo.Exists || fileInfo.Length == 0)
            {
                return null;
            }
            XmlSerializer xml = new XmlSerializer(typeof(List<Weather>));
            var weathers = new List<Weather>();
            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
            {
                weathers = xml.Deserialize(fs) as List<Weather>;
            }
            return weathers?.FirstOrDefault(w => w.cityName.Equals(cityName));
        }

        async Task IWeatherRepository.AddWeatherAsync(Weather weather)
        {
            List<Weather> weathers = new List<Weather>();
            XmlSerializer xml = new XmlSerializer(typeof(List<Weather>));
            if (fileInfo.Exists)
            {
                using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open))
                {
                    weathers = (List<Weather>)xml.Deserialize(fs);
                }
            }
            weathers.Add(weather);
            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Create))
            {
                xml.Serialize(fs, weathers);
            }
        }

        async Task IWeatherRepository.RemoveWeatherAsync(Weather weather)
        {
            List<Weather> weathers = new List<Weather>();
            XmlSerializer xml = new XmlSerializer(typeof(List<Weather>));
            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Open))
            {
                weathers = (List<Weather>)xml.Deserialize(fs);
            }
            foreach (Weather w in weathers)
            {
                if (w.cityName.Equals(weather.cityName))
                {
                    weathers.Remove(w);
                    break;
                }
            }
            using (FileStream fs = new FileStream(fileInfo.FullName, FileMode.Create))
            {
                xml.Serialize(fs, weathers);
            }
        }
    }
}
