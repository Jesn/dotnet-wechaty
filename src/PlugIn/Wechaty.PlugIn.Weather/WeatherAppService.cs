using Newtonsoft.Json;
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
    public class WeatherAppService : PlugInApplicationService
    {
        public async Task<OutPutDTO> GetWeatherAsync(InputDto input)
        {
            OutPutDTO model = new OutPutDTO();

            // get city info
            string cityApiUrl = WeatherConst.HefengApiUrl + $"v2/city/lookup?location={input.City}&mode=fuzzy&key={input.Token}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(cityApiUrl);

                var responseContent = await response.Content.ReadAsStringAsync();
                JObject _object = JsonConvert.DeserializeObject<JObject>(responseContent);
                if (_object["code"].ToString() != "200")
                {
                    return model;
                }
                model.CityId = _object.First["id"].ToString();
                model.CityName = _object.First["name"].ToString();

                model.H5Url = $"https://widget-page.heweather.net/h5/index.html?bg=1&md=012345&lc={model.CityId}&key={input.Token}";

                string nowApiUrl = $"https://devapi.heweather.net/v7/weather/now?location={model.CityId}&key={input.Token}";

                var nowResponse = await client.GetAsync(nowApiUrl);
                var nowResponseContent = await response.Content.ReadAsStringAsync();
                JObject _objectNow = JsonConvert.DeserializeObject<JObject>(nowResponseContent);
                if (_objectNow["code"].ToString() == "200")
                {
                    model.Text = _objectNow["now"]["text"].ToString();
                    model.FeelsLike = Convert.ToDouble(_objectNow["now"]["feelsLike"].ToString());
                    model.Humidity = Convert.ToDouble(_objectNow["now"]["humidity"].ToString());
                }
            }

            return model;
        }
    }
}
