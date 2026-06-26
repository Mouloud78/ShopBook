using Microsoft.EntityFrameworkCore;
using ShopBookWeb.Models;

namespace ShopBookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
    }
}
