using FilmDatabaseSysteem.Data;
using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace FilmDatabaseSysteem.Pages
{

    public class LoginModel : PageModel
    {
        private readonly FilmDbContext _dbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginModel(FilmDbContext dbContext,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public string? CurrentPage { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public void OnGet()
        {
            CurrentPage = "/";
        }

        [Authorize]
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Email);
               // var newToken = _userManager.SetAuthenticationTokenAsync;
                var userId = user?.Id;
                if (user != null && await _userManager.CheckPasswordAsync(user, Password))
                {
                    await _signInManager.SignInAsync(user, false);
                    var claims = new List<Claim>{new Claim(ClaimTypes.Name, user.UserName),new Claim(ClaimTypes.Email, user.Email)};

                    var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var userPrincipal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                    return RedirectToPage("/Index");

                }
                else
                {
                    ModelState.AddModelError("", "Ongeldig email of wachtwoord.");
                }

            }
            return Page();
        }
    }
}

