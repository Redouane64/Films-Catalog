
using Films.Website.Domain;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Films.WebSite.Data
{
    public class FilmsDbContext : IdentityDbContext<User>
    {
        public FilmsDbContext(DbContextOptions<FilmsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignore unecessary auto-generated identity tables
            // Keep only necessary tables and columns for this project.
            modelBuilder.Ignore<IdentityRole>()
                        .Ignore<IdentityRoleClaim<string>>()
                        .Ignore<IdentityUserRole<string>>()
                        .Ignore<IdentityUserClaim<string>>()
                        .Ignore<IdentityUserLogin<string>>()
                        .Ignore<IdentityUserToken<string>>();

            modelBuilder.Entity<User>()
                        .Ignore(e => e.AccessFailedCount)
                        .Ignore(e => e.ConcurrencyStamp)
                        .Ignore(e => e.EmailConfirmed)
                        .Ignore(e => e.LockoutEnabled)
                        .Ignore(e => e.LockoutEnd)
                        .Ignore(e => e.PhoneNumberConfirmed)
                        .Ignore(e => e.TwoFactorEnabled);

            modelBuilder.Entity<User>()
                .ToTable("users", "catalog")
                .HasMany(e => e.Films)
                .WithOne(e => e.Creator);

        }

        public DbSet<Film> Films { get; set; }

    }
}
