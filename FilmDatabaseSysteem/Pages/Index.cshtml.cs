using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmDatabaseSysteem.Pages
{
    //[Authorize]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public List<Movie> Movies { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }


        public IndexModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            TMDBService service = new TMDBService();
            var trendingMovies = await service.GetTrendingMovies();
            ViewData["TrendingMovies"] = trendingMovies;

            if (!string.IsNullOrEmpty(SearchString))
            {
                var movies = (List<Movie>)ViewData["TrendingMovies"];
                movies = movies.Where(m => m.Title.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                ViewData["TrendingMovies"] = movies;
            }

            var user = await _signInManager.UserManager.GetUserAsync(User);
            if (user != null)
            {
                ViewData["UserName"] = user.UserName;
            }


            return Page();
        }
    }
}