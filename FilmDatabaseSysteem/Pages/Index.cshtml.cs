using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FilmDatabaseSysteem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Movie> Movies { get; set; }
        public string? SearchString { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        public IActionResult OnGet()
        {
            TMDBService service = new TMDBService();

            var trendingMovieIds = service.GetTrendingMovies().Result;
            Movies = new List<Movie>();

            //foreach (var movieId in trendingMovieIds)
            //{
            //    var item = service.GetMovieDetails(movieId).Result;
            //    Movie movie = JsonConvert.DeserializeObject<Movie>(item);
            //    Movies.Add(movie);
            //}

            return Page();
        }

        public async Task OnPostAsync()
        {
            var SearchString = Request.Form["SearchString"];
            // hier moet de api call nog komen als we weten hoe die eruit ziet

            //Film = await db.Film.Where(s => s.Titel.Contains(SearchString)).ToListAsync();
        }

    }
}