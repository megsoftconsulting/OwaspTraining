using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Day08.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public virtual DbSet<TrustedUrl> TrustedUrls { get; set; }
    }
}