using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.WebAPI.Models
{
    public static class UrlProvider
    {
        public static string GetTopStoriesUrl()
        {
            return "https://hacker-news.firebaseio.com/v0/topstories.json";
        }

        public static string GetBestStoriesUrl()
        {
            return "https://hacker-news.firebaseio.com/v0/topstories.json";
        }

        public static string GetNewStoriesUrl()
        {
            return "https://hacker-news.firebaseio.com/v0/topstories.json";
        }

        public static string GetStoryById(int id)
        {
            return "https://hacker-news.firebaseio.com/v0/topstories.json";
        }


    }
}
