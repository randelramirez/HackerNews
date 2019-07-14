using HackerNews.WebAPI.Models;
using Newtonsoft.Json;
using System;
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
            var ids = JsonConvert.DeserializeObject<IEnumerable<int>>(GetTopStories().Result);
            var stories = new List<Story>();
            foreach (var id in ids)
            {
                stories.Add(GetStoryById(id));
            }

            return JsonConvert.SerializeObject(stories);
        }

        public async Task<string> GetStoriesAsync()
        {
            var ids = JsonConvert.DeserializeObject<IEnumerable<int>>(await GetTopStories());
            var stories = new List<string>();
            foreach (var id in ids)
            {
                stories.Add(await GetStoryByIdAsync(id));
            }
            
            return JsonConvert.SerializeObject(stories);
        }

        private async Task<string> GetTopStories()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(GetTopStoriesUrl());
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        private async Task<string> GetBestStories()
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(GetBestStoriesUrl());
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        private Story GetStoryById(int id)
        {
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json").Result;
            response.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<Story>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        private async Task<string> GetStoryByIdAsync(int id)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private IEnumerable<Story> GetStories(IEnumerable<int> topStories)
        {
            return topStories.ToList().Select(s => GetStoryById(s));
        }
    }
}
