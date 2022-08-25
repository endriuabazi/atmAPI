
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



        public DbSet<Account> accounts { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Transaction> transactions { get; set; }

    }
}
