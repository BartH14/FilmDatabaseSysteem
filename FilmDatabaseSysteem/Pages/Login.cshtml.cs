using FilmDatabaseSysteem.Data;
using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FilmDatabaseSysteem.Pages
{
    public class LoginModel : PageModel
    {
        // wordt gebruikt om conditional de header te hiden/showen
        public string? CurrentPage { get; set; }

        //opzetten van DB connectie
        private readonly FilmDbContext _dbContext;
        public LoginModel(FilmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
            CurrentPage = "/login";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == Username && u.Password == Password);

            if (user != null)
            {
                // User is valid, redirect to the home page or some other protected page
                return RedirectToPage("/Index");
            }
            else
            {
                // User is not valid, add a new user to the database
                var newUser = new User
                {
                    Username = Username,
                    Password = Password
                };

                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                // Redirect to the home page or some other protected page
                return RedirectToPage("/Index");
            }
        }
    }
}

