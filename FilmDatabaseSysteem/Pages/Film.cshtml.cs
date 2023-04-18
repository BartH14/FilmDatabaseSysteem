using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FilmDatabaseSysteem.Pages
{
    //[Authorize]
    public class FilmModel : PageModel
    {
        public Movie movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int movieId)
        {
            TMDBService service = new TMDBService();

            var response = service.GetMovieDetails(movieId).Result;
            movie = JsonConvert.DeserializeObject<Movie>(response);

            return Page();
        }
    }
}