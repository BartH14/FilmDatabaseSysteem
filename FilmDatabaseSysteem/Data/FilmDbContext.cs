using System;
using Microsoft.EntityFrameworkCore;
using FilmDatabaseSysteem.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;

namespace FilmDatabaseSysteem.Data
{
    public class FilmDbContext : DbContext
    {
        public FilmDbContext(DbContextOptions options) : base(options)
        {
        }
        //public DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles")
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins")
                .HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens")
                .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
        }

        
    }
}

