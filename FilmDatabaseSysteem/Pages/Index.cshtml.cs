using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FilmDatabaseSysteem.Pages
{
    //[Authorize]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public List<Movie> Movies { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }


        public IndexModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> OnGetAsync()
        {
            TMDBService service = new TMDBService();
            var trendingMovies = await service.GetTrendingMovies();
            ViewData["TrendingMovies"] = trendingMovies;
            //ViewData["UserName"] = "test"; 
            //var user2 = await _userManager.GetUserAsync(User);

            if (!string.IsNullOrEmpty(SearchString))
            {
                var movies = (List<Movie>)ViewData["TrendingMovies"];
                movies = movies.Where(m => m.Title.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();
                ViewData["TrendingMovies"] = movies;
            }


            //string userName = User.FindFirst(ClaimTypes.Name)?.Value;
            //string userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (userId != null)
            //{
            //    var user = await _userManager.FindByIdAsync(userId);
            //    ViewData["UserName"] = user.UserName;
            //}


            return Page();
        }
    }
}