using FilmDatabaseSysteem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FilmDatabaseSysteem.Pages
{
    [Authorize]
    public class userGegevensModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly FilmDbContext _dbContext;
        [BindProperty]
        public InputModel Input { get; set; }

        public userGegevensModel(FilmDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            //var testUser = User.Identity.Name;
            //var user = await _userManager.GetUserAsync(User);
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            if (UserEmail != null)
            {
                var user = await _userManager.FindByEmailAsync(UserEmail);
                // ...
            }
            var VDMail = UserEmail;
            ViewData["UserEmail"] = VDMail;

            return Page();
        }



        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string? Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string? Password { get; set; }

        }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
        //        var result = await _userManager.CreateAsync(user, Input.Password);
        //        if (result.Succeeded)
        //        {
        //            await _signInManager.SignInAsync(user, isPersistent: false);
        //            return RedirectToPage("/Index");
        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    return Page();
        //}

        [Authorize]
        public async Task<IActionResult> OnPostAsync()
        {
          
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page or return an error
                return RedirectToPage("/Login");
            }

            // get the current user
            var user = await _userManager.GetUserAsync(User);
            // update the user's password
            if (!string.IsNullOrEmpty(Input.Password))
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, user.PasswordHash, Input.Password);
                if (!passwordChangeResult.Succeeded)
                {
                    foreach (var error in passwordChangeResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return RedirectToPage();
        }

    }
}
