using System.Threading.Tasks;
using Wechaty.PlugIn.Weather;
using Xunit;
using Shouldly;
using System.Net.Http;
using System.Linq;

namespace Wechaty.PlugIn.Tests.PlugInTests
{
    public class WeatherPluginService_Tests : WechatyPlugInTestBase
    {
        private readonly WeatherPlugInService _weatherPlugInService;

        public WeatherPluginService_Tests()
        {
            _weatherPlugInService = GetRequiredService<WeatherPlugInService>();
        }


        [Fact]
        public async Task GetWeather()
        {

            var result = await _weatherPlugInService.GetWeatherAsync(
                new InputDto()
                {
                    City = "beijing",
                    Token = "c7601763d0a241568f75213509012c48"
                });
            result.CityName.ShouldBe("北京");
        }

    }
}
