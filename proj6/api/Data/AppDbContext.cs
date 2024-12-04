using Microsoft.EntityFrameworkCore;
using proj6.Models;

namespace proj6.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja encji Ticket
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(t => t.Description)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(t => t.Status)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(t => t.Priority)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(t => t.CreatedAt)
                    .IsRequired();
                entity.Property(t => t.UpdatedAt)
                    .IsRequired();

                // Relacja jeden Ticket - wiele Comments
                entity.HasMany(t => t.Comments)
                      .WithOne(c => c.Ticket)
                      .HasForeignKey(c => c.TicketId)
                      .OnDelete(DeleteBehavior.Cascade); // Usuwanie kaskadowe
            });

            // Konfiguracja encji Comment
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Author)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(c => c.Text)
                    .IsRequired()
                    .HasMaxLength(300);
                entity.Property(c => c.CreatedAt)
                    .IsRequired();
            });
        }
    }
}
