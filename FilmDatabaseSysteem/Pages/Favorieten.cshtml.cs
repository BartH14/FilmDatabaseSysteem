using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FilmDatabaseSysteem.Pages
{
    [Authorize]
    public class FavorietenModel : PageModel
    {
        public Favorieten favorieten { get; set; }
        public List<Movie> Movies { get; set; }

        public IActionResult OnGet()
        {
            TMDBService service = new TMDBService();

            favorieten = new Favorieten(1, new List<int>() { 85936, 1091671, 603 });
            Movies = new List<Movie>();

            foreach (var movieId in favorieten.MovieIds)
            {
                var item = service.GetMovieDetails(movieId).Result;
                Movie movie = JsonConvert.DeserializeObject<Movie>(item);
                Movies.Add(movie);
            }

            return Page();
        }
    }
}