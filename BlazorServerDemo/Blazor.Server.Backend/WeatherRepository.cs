using Blazor.Server.Backend.Data;
using Blazor.Server.Shared.Interfaces;
using Blazor.Shared.Models;

namespace Blazor.Server.Backend;

public class WeatherRepository : IWeatherRepository
{
    private readonly Database database;

    public WeatherRepository(string connectionString)
        : this(new Database(connectionString))
    {
    }

    public WeatherRepository(Database database)
    {
        this.database = database;
    }

    private static readonly string[] Summaries = new[] {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm",
        "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<WeatherForecast[]> GetForecastsAsync(DateTime startDate)
    {
        WeatherForecast[] result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();

        return Task.FromResult(result);
    }

    // or Dapper ... whatever
    //public List<MODEL> Get()
    //{
    //    return database.Query<MODEL>("NAME_OF_STORED_PROCEDUCRE");
    //}
}
