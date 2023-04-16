using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FilmDatabaseSysteem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public List<Movie> Movies { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            TMDBService service = new TMDBService();
            var trendingMovies = await service.GetTrendingMovies();
            ViewData["TrendingMovies"] = trendingMovies;

            if (!string.IsNullOrEmpty(SearchString))
            {
                var movies = (List<Movie>)ViewData["TrendingMovies"];
                movies = movies.Where(m => m.Title.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                ViewData["TrendingMovies"] = movies;
            }

            return Page();
        }

        //public IActionResult OnGetSearch(string query)
        //{
        //    var movies = (List<Movie>)ViewData["TrendingMovies"];

        //    if (!string.IsNullOrEmpty(query))
        //    {
        //        movies = movies.Where(m => m.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        //    }

        //    ViewData["TrendingMovies"] = movies;
        //    return Page();
        //}
    }
}