using Microsoft.EntityFrameworkCore;
using proj5API.Models;

namespace proj5API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet dla modeli
        public DbSet<Book> Books { get; set; }
    }
}
