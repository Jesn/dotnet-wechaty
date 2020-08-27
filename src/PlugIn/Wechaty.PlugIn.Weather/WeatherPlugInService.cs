using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Wechaty.Plugin.Base;
using Wechaty.PlugIn.Weather;

namespace Wechaty.PlugIn
{
    /// <summary>
    /// 天气插件
    /// </summary>
    public class WeatherPlugInService : PlugInApplicationService
    {

        private readonly IHttpClientFactory _clientFactory;

        public WeatherPlugInService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<OutPutDTO> GetWeatherAsync(InputDto input)
        {
            OutPutDTO model = new OutPutDTO();

            // get city info
            string cityApiUrl = WeatherConst.HefengApiUrl + $"v2/city/lookup?location={input.City}&mode=fuzzy&key={input.Token}&gzip=n";

            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, cityApiUrl);
           
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                JObject cityObj = JObject.Parse(await response.Content.ReadAsStringAsync());
                model.CityId = cityObj["location"][0]["id"].ToString();
                model.CityName = cityObj["location"][0]["name"].ToString();

                model.H5Url = $"https://widget-page.heweather.net/h5/index.html?bg=1&md=012345&lc={model.CityId}&key={input.Token}";

                string nowApiUrl = $"https://devapi.heweather.net/v7/weather/now?location={model.CityId}&key={input.Token}&gzip=n";
                var requestNow = new HttpRequestMessage(HttpMethod.Get, nowApiUrl);
                var responseNow = await client.SendAsync(requestNow);
                if (responseNow.IsSuccessStatusCode)
                {
                    JObject _objectNow = JObject.Parse(await responseNow.Content.ReadAsStringAsync());

                    model.Text = _objectNow["now"]["text"].ToString();
                    model.FeelsLike = Convert.ToDouble(_objectNow["now"]["feelsLike"].ToString());
                    model.Humidity = Convert.ToDouble(_objectNow["now"]["humidity"].ToString());
                }
            }
            return model;
        }
    }
}
