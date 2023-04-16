using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FilmDatabaseSysteem.Models;

public class TMDBService
{
    private readonly string ApiKey;
    private readonly HttpClient _httpClient;

    public TMDBService()
    {
        this.ApiKey = "b4d9d59d46e5dabef3fe35c270a43af5";
        this._httpClient = new HttpClient();
        this._httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
    }

    public async Task<dynamic> GetMovieDetails(int movieId)
    {
        var response = await _httpClient.GetStringAsync($"movie/{movieId}?api_key={ApiKey}");
        return response;
    }

    public async Task<dynamic> GetSimilarMovie(int movieId)
    {
        var response = await _httpClient.GetStringAsync($"/movie/{movieId}/similar?api_key={ApiKey}");
        return response;
    }

    public async Task<dynamic> GetRecommendedMovie(int movieId)
    {
        var response = await _httpClient.GetStringAsync($"/movie/{movieId}/recommendations?api_key={ApiKey}");
        return response;
    }

    public async Task<dynamic> GetTrendingMovies()
    {
        var response = await _httpClient.GetStringAsync($"trending/movie/week?api_key={ApiKey}");
        var json = JObject.Parse(response);
        var movies = JsonConvert.DeserializeObject<List<Movie>>(json["results"].ToString());
        return movies;
    }
}