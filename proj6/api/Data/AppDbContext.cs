using Microsoft.EntityFrameworkCore;
using proj6.Models;

namespace proj6.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
