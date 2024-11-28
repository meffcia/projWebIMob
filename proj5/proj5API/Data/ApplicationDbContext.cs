using Microsoft.EntityFrameworkCore;
using proj5API.Models;

namespace proj5API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet dla modeli
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<AuthorWriter> AuthorWriters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthorWriter>(entity =>
            {
                entity.HasKey(aw => new { aw.AuthorId, aw.WriterId }); // Klucz złożony

                // Relacja AuthorWriter -> Author
                entity.HasOne(aw => aw.Author)
                    .WithMany(a => a.AuthorWriters)
                    .HasForeignKey(aw => aw.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relacja AuthorWriter -> Writer
                entity.HasOne(aw => aw.Writer)
                    .WithMany(w => w.AuthorWriters)
                    .HasForeignKey(aw => aw.WriterId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Opcjonalnie: nazwa tabeli w bazie danych
                entity.ToTable("AuthorWriters");
            });

            // Inne konfiguracje (opcjonalnie)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithOne(b => b.Review)
                .HasForeignKey<Review>(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
