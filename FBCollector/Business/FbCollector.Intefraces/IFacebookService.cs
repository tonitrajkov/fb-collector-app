using System.Threading.Tasks;

namespace FbCollector.Intefraces
{
    public interface IFacebookService
    {
        Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null);

        Task PostAsync(string accessToken, string endpoint, object data, string args = null);

        void GetPageFeed(string accessToken, string endpoint, string args = null);
    }
}
