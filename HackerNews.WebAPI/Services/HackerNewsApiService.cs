using HackerNews.WebAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static HackerNews.WebAPI.Models.UrlProvider;

namespace HackerNews.WebAPI.Services
{
    public class HackerNewsApiService
    {
        public string GetStories()
        {
            var ids = JsonConvert.DeserializeObject<IEnumerable<int>>(GetTopStoriesAsync().GetAwaiter().GetResult());
            var stories = new List<Story>();
            foreach (var id in ids)
            {
                stories.Add(GetStoryById(id));
            }

            return JsonConvert.SerializeObject(stories);
        }

        public async Task<string> GetStoriesAsync(int numberOfStories = 500)
        {
            var ids = JsonConvert.DeserializeObject<IEnumerable<int>>(await GetTopStoriesAsync());

            if (numberOfStories > 0)
            {
                ids = ids.Take(numberOfStories);
            }

            var tasks = ids.Select(async id => await GetStoryByIdAsync(id));
            var stories = await Task.WhenAll(tasks);
            return JsonConvert.SerializeObject(stories);
        }

        private async Task<string> GetTopStoriesAsync()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(TopStoriesUrl);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        private async Task<string> GetBestStoriesAsync()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(BestStoriesUrl);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }



        private async Task<string> GetStoryByIdAsync(int id)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(CreateItemUrl(id));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        // USE Model returns on applications like Xamarin, because web api is suppose to return json
        private Story GetStoryById(int id)
        {
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(CreateItemUrl(id)).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<Story>(response.Content.ReadAsStringAsync().Result);
            return result;
        }
    }
}
