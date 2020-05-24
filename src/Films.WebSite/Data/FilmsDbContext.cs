using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
