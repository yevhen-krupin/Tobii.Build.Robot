using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Tobii.Build.Robot.Rest
{
    public class RestClient : IRestClient
    {
        private readonly Uri _baseUrl;
        private readonly string _credentials;
        private readonly Encoding _defaultEncoding = Encoding.UTF8;
        private readonly MediaType _mediaType;

        public RestClient(string baseUrl,  string userName, string password, MediaType mediaType = null)
        {
            _mediaType = mediaType ?? MediaType.Json;
            _baseUrl = new Uri(baseUrl);
            _credentials = Convert.ToBase64String(_defaultEncoding.GetBytes($"{userName}:{password}"));
        }

        public async Task<T> Get<T>(string url)
        {
            using (var client = ObtainClient())
            {
                using (var result = await client.GetAsync(url).ConfigureAwait(false))
                {
                    return await ConvertToResult<T>(result);
                }
            }
        }

        private static async Task<T> ConvertToResult<T>(HttpResponseMessage result)
        {
            var response = await result.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(response);
            return obj;
        }

        private HttpClient ObtainClient()
        {
            var client = new HttpClient {BaseAddress = _baseUrl};
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType.Value));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credentials);
            return client;
        }


        public async Task<TResponse> Post<TResponse>(string url, string body)
        {
            var content = new StringContent(body, _defaultEncoding, "application/xml");
            using (var client = ObtainClient())
            {
                using (var result = await client.PostAsync(url, content).ConfigureAwait(false))
                {
                    if (!result.IsSuccessStatusCode)
                    {
                        var str = await result.Content.ReadAsStringAsync();
                        throw new InvalidOperationException(str);
                    }
                    return await ConvertToResult<TResponse>(result);
                }
            }
        }
    }
}