using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FilmDatabaseSysteem.Pages
{
    public class RegistratieModel : PageModel
    {
        public string? CurrentPage { get; set; }

        public void OnGet()
        {
            CurrentPage = "/registratie";
        }
    }
}
