using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FilmDatabaseSysteem.Pages
{
    public class FilmModel : PageModel
    {
        public Movie movies { get; set; }
        public List<Movie> Movies { get; set; }

        public IActionResult OnGet()
        {
            TMDBService service = new TMDBService();

            movies = new Movie();
            Movies = new List<Movie>();

            //foreach (var movieId in movies.MovieIds)
            //{
                var item = service.GetMovieDetails(550).Result;
                Movie movie = JsonConvert.DeserializeObject<Movie>(item);
             //   Movies.Add(movie);
            //}

            return Page();
        }
    }
}