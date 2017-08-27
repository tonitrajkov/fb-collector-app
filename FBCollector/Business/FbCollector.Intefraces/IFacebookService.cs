using System.Threading.Tasks;
using FbCollector.Models;

namespace FbCollector.Intefraces
{
    public interface IFacebookService
    {
        Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null);

        Task PostAsync(string accessToken, string endpoint, object data, string args = null);

        void GetPageFeed(string accessToken, string endpoint, string args = null);

        FbPageModel GetPageDetails(string endpoint, string args = null);

        FbAccessTokenModel GetAccessToken(string graphVersion = null);

        string GetPageFansByCounty(string endpoint);

        void ReIndexFeedImages(string endpoint, string args = null);
    }
}
