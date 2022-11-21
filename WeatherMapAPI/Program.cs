using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;



namespace WeatherMapAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string key = config.GetSection("Keys").GetSection("WeatherMapApi").Value;

            var lat = "21.3099";
            var lon = "157.8581";

            var client = new HttpClient();
            var URL = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={key}&units=imperial";
            var weatherResponse = client.GetStringAsync(URL).Result;

            var weatherJSON = JObject.Parse(weatherResponse);

            var main = (JObject)weatherJSON.GetValue("main");
            var temp = (JToken)main.GetValue("temp");
            var humid = (JToken)main.GetValue("humidity");



            var weather = (JObject)weatherJSON.GetValue("weather")[0];
            var skys = (JToken)weather.GetValue("main");

            Console.WriteLine($"The todays forcast in Honolulu Hawaii is {temp} degress." +
                              $"The humidity is {humid}, so not too bad." +
                              $"Expect todays skys to be {skys}.");




        }
    }
}