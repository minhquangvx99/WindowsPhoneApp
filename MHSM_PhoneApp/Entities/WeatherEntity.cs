using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MHSM_PhoneApp.Entities
{
    class WeatherDataEntity
    {
        public string stationName { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public DateTime localDateTime { get; set; }
        public bool isDayTime { get; set; }
        public string temperature { get; set; }
        public string windDirection { get; set; }
        public string windSpeed { get; set; }
        public string cloudCover { get; set; }
        public string relativeHumidity { get; set; }
        public string pressure { get; set; }
        public string dewPoint { get; set; }
        public string visibility { get; set; }
        public Icon icon { get; set; }
        public RealRainAWSData realRainAWSData { get; set; }
        public UvIndex uvIndex { get; set; }
    }

    class WeatherDetailData
    {
        public string sessionOfDay { get; set; }
        public string dayOfWeek { get; set; }
        public DateTime dateForeCast { get; set; }
        public string date { get; set; }
        public string hourPeriod { get; set; }
        public Rain rain { get; set; }
        public string rain24h { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string temperature { get; set; }
        public string temperatureMax { get; set; }
        public string temperatureMin { get; set; }
        public string pressure { get; set; }
        public string dewPoint { get; set; }
        public string cloudCover { get; set; }
        public string cloudCoverValue { get; set; }
        public string relativeHumidity { get; set; }
        public object uvIndex { get; set; }
        public Icon icon { get; set; }
    }

    public class Rain
    {
        public double totalIn6h { get; set; }
        public string text { get; set; }
    }

    class Icon
    {
        public BitmapImage iconWeather { get; set; }
        public string iconLink { get; set; }
        public string description { get; set; }
        public string code { get; set; }
    }

    class RealRainAWSData
    {
        public string stationName { get; set; }
        public DateTime localDateTime { get; set; }
        public string precip1h { get; set; }
        public string text { get; set; }
    }

    class UvIndex
    {
        public string uvi { get; set; }
        public string text { get; set; }
    }

    class WeatherEntity
    {
        public List<Data> Data { get; set; }
    }
    class Data
    {
        public string ProvinceNo { get; set; }
        public string ProvinceName { get; set; }
        public List<Detail> detail { get; set; }
    }
    class Detail
    {
        public string DayOfWeek { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Summary { get; set; }
        public int WeatherForecastEnum { get; set; }
    }
    class NowData
    {
        public string ProvinceNo { get; set; }
        public string ProvinceName { get; set; }
        public string Summary { get; set; }
    }
    class NowWeekData
    {
        public string DayOfWeek { get; set; }
        public string Summary { get; set; }
    }

}
