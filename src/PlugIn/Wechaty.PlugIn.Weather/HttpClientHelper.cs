using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wechaty.PlugIn.Weather
{
    public class HttpClientHelper
    {
        // asp.Net Core 推荐的方式
        private readonly IHttpClientFactory _clientFactory;

        public HttpClientHelper(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<string> SendAsyncCore(string url, HttpMethod method, string reqParams = "")
        {
            using (HttpRequestMessage reqMessage = new HttpRequestMessage(method, url))
            {
                reqMessage.Content = new StringContent(reqParams, Encoding.UTF8, "application/json");

                using (var resMessage = await _clientFactory.CreateClient("kmauth").SendAsync(reqMessage))
                {
                    resMessage.EnsureSuccessStatusCode();
                    return await resMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
        }
    }
}
