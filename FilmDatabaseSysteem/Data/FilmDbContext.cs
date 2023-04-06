using System;
using Microsoft.EntityFrameworkCore;
using FilmDatabaseSysteem.Models;

namespace FilmDatabaseSysteem.Data
{
    public class FilmDbContext : DbContext
    {
        public FilmDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = default!;
    }
}

