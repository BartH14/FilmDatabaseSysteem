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
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        //opzetten van DB connectie
        private readonly FilmDbContext _dbContext;
        public LoginModel(FilmDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnGet()
        {
            CurrentPage = "/";
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);

            if (user != null)
            {
                // User is valid, redirect to the home page or some other protected page
                return RedirectToPage("/home");
            }
            else
            {
                if (Email == null || Password == null)
                {
                    // display error , authentication 
                    return null;
                }
                else
                {
                    // User is not valid, add a new user to the database
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
    }
}

