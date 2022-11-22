using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using CapstoneProject.Models;
using Newtonsoft.Json;

namespace CapstoneProject.Controllers
{
    public class RAWG
    {
        private const string API_URL = "https://api.rawg.io/api/";
        private const string API_KEY = "2b696e8994c64d778bb9beddfe0f6ef8";

        private static async Task<GameResult> QueryGame(string gameName)
        {
            string RequestUrl = API_URL + "games/" + gameName + "?key=" + API_KEY; 
            string JsonResponse = "";

            using (HttpClient Client = new HttpClient()) 
            {
                Task<HttpResponseMessage> TaskResponse = Client.GetAsync(RequestUrl);
                TaskResponse.Wait();
                JsonResponse = await TaskResponse.Result.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<GameResult>(JsonResponse);
        }

        public GameResult GetGameData(string gameName) 
        {
            Task<GameResult> GameQueryResult = Task.Run(() => QueryGame(gameName));
            GameQueryResult.Wait();
            GameResult gameResult = GameQueryResult.Result;
            if (gameResult.esrb_rating == null) gameResult.esrb_rating = new ESRBRating() { Id = 0, Name = "N/A", Slug = "NA" };
            return gameResult;
        }

        private static async Task<SearchResult> QueryGameSearch(string gameName)
        {
            string RequestUrl = API_URL + "games?key=" + API_KEY + "&search=" + gameName;
            string JsonResponse = "";

            using (HttpClient Client = new HttpClient())
            {
                Task<HttpResponseMessage> TaskResponse = Client.GetAsync(RequestUrl);
                TaskResponse.Wait();
                JsonResponse = await TaskResponse.Result.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<SearchResult>(JsonResponse);
        }

        public SearchResult GetSearchResult(string gameName) 
        {
            Task<SearchResult> SearchQueryResult = Task.Run(() => QueryGameSearch(gameName));
            SearchQueryResult.Wait();
            SearchResult searchResult = SearchQueryResult.Result;
            foreach (GameResult gameResult in searchResult.Results)
            {
                if (gameResult.esrb_rating == null) gameResult.esrb_rating = new ESRBRating() { Id = 0, Name = "N/A", Slug = "NA" };
            }
            return searchResult;
        }
    }
}
