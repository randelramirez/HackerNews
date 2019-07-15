namespace HackerNews.WebAPI.Models
{
    public static class UrlProvider
    {
        private const string baseApiUrl = "https://hacker-news.firebaseio.com/v0/";

        public static string TopStoriesUrl => baseApiUrl + "topstories.json";

        public static string BestStoriesUrl => baseApiUrl + "beststories.json";
  
        public static string NewStoriesUrl => baseApiUrl + "newstories.json";

        public static string CreateItemUrl(int id) => baseApiUrl + $"item/{id}.json";
    }
}
