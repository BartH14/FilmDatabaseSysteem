using Microsoft.EntityFrameworkCore;
using FilmDatabaseSysteem.Data;
using FilmDatabaseSysteem.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<FilmDbContext>(opt => opt.UseSqlite("Data Source=FilmDatabaseSysteem.Data.db"));

// Add identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<FilmDbContext>();

// Add SignInManager service
builder.Services.AddScoped<SignInManager<IdentityUser>>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
   .AddCookie(options =>
   {
       options.Cookie.HttpOnly = true;
       options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
       options.SlidingExpiration = true;
       options.ClaimsIssuer = "FilmDatabaseZuyd";
       options.Events = new CookieAuthenticationEvents
       {
           OnValidatePrincipal = async context =>
           {
               // Add the NameIdentifier claim to the user's identity
               var identity = (ClaimsIdentity)context.Principal.Identity;
               var userId = context.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
               if (userId != null)
               {
                   identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
               }
               await Task.CompletedTask;
           }
       };
   });

builder.Services.Configure<IdentityOptions>(options =>
{
 options.Password.RequireDigit = false;
 options.Password.RequireLowercase = false;
 options.Password.RequireNonAlphanumeric = false;
 options.Password.RequireUppercase = false;
 options.Password.RequiredLength = 1;
 options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<FilmDbContext>();
    context.Database.EnsureCreated();
    //SeedData.Initialize(context);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();

app.Run();


