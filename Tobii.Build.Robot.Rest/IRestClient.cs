using System.Threading.Tasks;

namespace Tobii.Build.Robot.Rest
{
    public interface IRestClient
    {
        Task<T> Get<T>(string url);
        Task<TResponse> Post<TReqeust, TResponse>(string url, TReqeust request);
    }
}