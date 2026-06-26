using Microsoft.EntityFrameworkCore;
using ShopBookWeb.Models;

namespace ShopBookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Action" },
                new Category { Id = 2, Name = "ASciFi" },
                new Category { Id = 3, Name = "History" }

                );
        }
    }
}
