using FilmDatabaseSysteem.Data;
using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FilmDatabaseSysteem.Pages
{
    public class RegistratieModel : PageModel
    {
        public string? CurrentPage { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        //opzetten van DB connectie
        private readonly FilmDbContext _dbContext;

        public RegistratieModel(FilmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            CurrentPage = "/registratie";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var newUser = new User
            {
                Email = Email,
                Password = Password
                //Role = Role
            };
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();
            // Redirect to the home page or some other protected page
            return RedirectToPage("/home");
        }
    }
}
