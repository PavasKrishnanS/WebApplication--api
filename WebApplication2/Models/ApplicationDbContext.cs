using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class ApplicationDbContext : DbContext
    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // Initialize additional properties if needed
        }

        public DbSet<Customer> Customers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .ToTable("customer")
                .HasKey(c => c.Id);
        }
    }

}
