using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FbCollector.Intefraces;
using FbCollector.Models;
using Newtonsoft.Json;

namespace FbCollector.Services
{
    public class FacebookService : IFacebookService
    {
        private readonly HttpClient _httpClient;
        private List<FbFeedModel> _feeds;
        private string _pageUrlId;
        private readonly PageFeedService _pageFeedService;

        public FacebookService(PageFeedService pageFeedService)
        {
            _feeds = new List<FbFeedModel>();
            _pageFeedService = pageFeedService;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://graph.facebook.com/")
            };

            _httpClient
                .DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null)
        {
            // "454603501363720/feed?fields=message,id,link,type,created_time,full_picture&limit=100&access_token=EAACEdEose0cBALjxT2OYqAxmkYwRpeTisqUe6aT0ZBebFD5waTku0ARvfresEM4WQL6SosEXyRQZBDdAU5gpwdDcY4o2DKvaLym0NxFcqUkm8Uz052Mh4dn5qV2EHj6GeRSv7IvSo8FHA38nmFI3iESaRnISCwwOFEaN3EerK9ARK2VHw8cMot4bvwgx4ZD";
            var param = string.Format("{0}/{1}&access_token={2}", endpoint, args, accessToken);
            var response = await _httpClient.GetAsync(param);
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task PostAsync(string accessToken, string endpoint, object data, string args = null)
        {
            var payload = GetPayLoad(data);

            await _httpClient.PostAsync("", payload);
        }

        public void GetPageFeed(string accessToken, string endpoint, string args = null)
        {
            _pageUrlId = endpoint;            
            var param = string.Format("{0}/{1}&access_token={2}", endpoint, args, accessToken);

            var response = _httpClient.GetAsync(param).Result;
            if (!response.IsSuccessStatusCode)
                return;

            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<FbFeedResponseModel>(jsonResult);

            if (model.data != null && model.data.Any())
            {
                _feeds.AddRange(model.data);
            }

            if (model.paging != null && model.paging.next != null)
            {
                GetPageFeedPaging(model.paging.next);
            }

            _pageFeedService.CreatePageFeed(_feeds, _pageUrlId);
        }

        private void GetPageFeedPaging(string url)
        {
            var response = _httpClient.GetAsync(url).Result;
            if (!response.IsSuccessStatusCode)
                return;

            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var model = JsonConvert.DeserializeObject<FbFeedResponseModel>(jsonResult);

            if (model.data != null && model.data.Any())
            {
                _feeds.AddRange(model.data);
            }

            if (model.paging != null && model.paging.next != null)
            {
                GetPageFeedPaging(model.paging.next);
            }
        }

        private static StringContent GetPayLoad(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
