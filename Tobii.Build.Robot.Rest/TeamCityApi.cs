using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tobii.Build.Robot.Model;

namespace Tobii.Build.Robot.Rest
{
    public class TeamCityApi : ICiCdServerApi
    {
        public IRestClient RestClient { get; }

        public TeamCityApi(IRestClient restClient)
        {
            RestClient = restClient;
        }
        
        public async Task<Branches> GetBranchesAsync(string projectId)
        {
            var request = $"/httpAuth/app/rest/projects/{projectId}/branches";
            return await RestClient.Get<Branches>(request);
        }

        public async Task<Builds> GetQueue()
        {
            var request = $"/httpAuth/app/rest/buildQueue";
            return await RestClient.Get<Builds>(request);
        }

        public async Task<Model.Build> GetBuild(string buildId)
        {
            var request = $"/httpAuth/app/rest/builds/id:{buildId}";
            return await RestClient.Get<Model.Build>(request);
        }

        public async Task<Builds> GetBuilds(string projectId)
        {
            var request = $"/httpAuth/app/rest/builds?locator=affectedProject:{projectId}";
            return await RestClient.Get<Builds>(request);
        }

        public async Task<BuildTypes> GetBuildTypes(string projectId)
        {
            var request = $"/httpAuth/app/rest/buildTypes?locator=affectedProject:{projectId}";
            return await RestClient.Get<BuildTypes>(request);
        }

        public async Task<Project> GetProjectAsync(string projectId)
        {
            var request = $"/httpAuth/app/rest/projects/{projectId}";
            return await RestClient.Get<Project>(request);
        }


        public async Task<Projects> GetProjectsAsync()
        {
            var request = $"/httpAuth/app/rest/projects";
            return await RequestFor<Projects>(request);
        }
    }

    public class MediaType
    {
        public static readonly MediaType Json = new MediaType("application/json");

        internal string Value;

        private MediaType(string mediaType)
        {
            Value = mediaType;
        }
    }

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