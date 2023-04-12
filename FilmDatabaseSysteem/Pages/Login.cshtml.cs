using FilmDatabaseSysteem.Data;
using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FilmDatabaseSysteem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly FilmDbContext _dbContext;

        // wordt gebruikt om conditional de header te hiden/showen
        public string? CurrentPage { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        //opzetten van DB connectie
        public LoginModel(FilmDbContext dbContext, SignInManager<IdentityUser> signInManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
            CurrentPage = "/";
        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return Page();
        //    //}

        //    //var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == Email && u.Password == Password);
        //    bool isAuthenticated = AuthenticateUser();

        //    if (isAuthenticated)
        //    {
        //        return RedirectToPage("/home");
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Invalid email or password.");
        //        return RedirectToPage("/login");
        //    }

        //}
        public bool AuthenticateUser()
        {
            // Replace this with your actual authentication logic
            List<User> validUsers = new List<User>
        {
        new User { Email = "user1@example.com", Password = "password1" },
        new User { Email = "user2@example.com", Password = "password2" },
        new User { Email = "user3@example.com", Password = "password3" }
         };

            User ?user = validUsers.FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user != null)
            {
                // User is authenticated
                return true;
            }
            else
            {
                // User is not authenticated
                return false;
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _signInManager.PasswordSignInAsync(Email, Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToPage("/Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return Page();
            }
        }
    }
}

