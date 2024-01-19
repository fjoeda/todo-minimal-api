namespace TodoMinimal
{
    public record WeatherData(DateOnly Date, float TemperatureC, string detail)
    {
        float TemperatureF = TemperatureC;
    }
    public class TodoServices
    {
        public record GetWeatherData(DateOnly Date, float TemperatureC)
        {

        }
    }
}
