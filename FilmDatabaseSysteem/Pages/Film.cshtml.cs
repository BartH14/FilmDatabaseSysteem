using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FilmDatabaseSysteem.Pages
{
    [Authorize]
    public class FilmModel : PageModel
    {
        public Movie movies { get; set; }
        public List<Movie> Movies { get; set; }

        public IActionResult OnGet()
        {
            TMDBService service = new TMDBService();

            movies = new Movie();
            Movies = new List<Movie>();
            int movieId = 38;

            if (movieId != null)
            {
                var item = service.GetMovieDetails(movieId).Result;
                Movie movie = JsonConvert.DeserializeObject<Movie>(item);
                Movies.Add(movie);
            }

            return Page();
        }
    }
}