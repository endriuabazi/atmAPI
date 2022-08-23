
using atmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace atmAPI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }



        public DbSet<account> accounts { get; set; }
        public DbSet<client> clients { get; set; }
        public DbSet<transaction> transactions { get; set; }

    }
}
