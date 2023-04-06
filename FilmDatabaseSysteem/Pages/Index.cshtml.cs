using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FilmDatabaseSysteem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string? SearchString { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            // onGet limit van 10 films met api call
        }

        public async Task OnPostAsync()
        {
            var SearchString = Request.Form["SearchString"];
            // hier moet de api call nog komen als we weten hoe die eruit ziet

            //Film = await db.Film.Where(s => s.Titel.Contains(SearchString)).ToListAsync();
        }

    }
}