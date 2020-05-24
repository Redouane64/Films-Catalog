using System;
using System.Collections.Generic;
using System.Text;

using FilmsLibrary.Domain;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Films.WebSite.Data
{
    public class FilmsDbContext : DbContext
    {
        public FilmsDbContext(DbContextOptions<FilmsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Uncomment when adding Identity
            // base.OnModelCreating(modelBuilder);
        }

        public DbSet<Film> Films { get; set; }
    }
}
