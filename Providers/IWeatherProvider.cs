using System.Collections.Generic;
using Chariots_of_Trails.Models;

namespace Chariots_of_Trails.Providers
{
    public interface IWeatherProvider
    {
        List<WeatherForecast> GetForecasts();
    }
}
