using Microsoft.AspNetCore.Mvc.RazorPages;
using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FilmDatabaseSysteem.Pages
{
    public class RandomModel : PageModel
    {
        public Movie movie { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Random random = new Random();
            TMDBService service = new TMDBService();

            while (true)
            {
                int movieId = random.Next(1, 714651);

                try
                {
                    var response = await service.GetMovieDetails(movieId);

                    if (response != null)
                    {
                        movie = JsonConvert.DeserializeObject<Movie>(response);
                        return Page();
                    }
                }
                catch (HttpRequestException ex)
                {
                    // ignore
                }
            }
        }
    }
}