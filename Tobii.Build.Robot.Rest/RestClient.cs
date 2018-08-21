using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tobii.Build.Robot.Rest
{
    public class RestClient : IRestClient
    {
        private readonly Uri _baseUrl;
        private readonly string _credentials;
        private readonly Encoding _defaultEncoding = Encoding.ASCII;
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


        public async Task<TResponse> Post<TReqeust, TResponse>(string url, TReqeust request)
        {
            var serialized = JsonConvert.SerializeObject(request);
            var content = new StringContent(serialized, _defaultEncoding);
            using (var client = ObtainClient())
            {
                using (var result = await client.PostAsync(url, content).ConfigureAwait(false))
                {
                    return await ConvertToResult<TResponse>(result);
                }
            }
        }
    }
}