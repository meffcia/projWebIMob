using Microsoft.EntityFrameworkCore;
using proj5.Domain.Models;

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

            // Konfiguracja tabeli AuthorWriter
            modelBuilder.Entity<AuthorWriter>(entity =>
            {
                entity.HasKey(aw => new { aw.AuthorId, aw.WriterId }); // Klucz złożony
                entity.ToTable("AuthorWriters");
            });

            // Konfiguracja relacji między Book i Review
            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(w => w.Content)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Konfiguracja dla encji Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.HasOne<Author>()
                    .WithMany()
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict); // Brak automatycznego kasowania autora

                entity.Property(b => b.Price)
                    .HasColumnType("decimal(18,2)"); // Precyzyjny typ dla ceny

                    
                entity.HasOne(b => b.Review)
                    .WithOne()
                    .HasForeignKey<Review>(r => r.BookId) // Klucz obcy w tabeli Review
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Konfiguracja dla encji Writer (opcjonalnie, jeśli potrzebujesz)
            modelBuilder.Entity<Writer>(entity =>
            {
                entity.HasKey(w => w.Id);

                entity.Property(w => w.Name)
                    .IsRequired()
                    .HasMaxLength(100); // Maksymalna długość imienia

                entity.Property(w => w.Surname)
                    .IsRequired()
                    .HasMaxLength(100); // Maksymalna długość nazwiska
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(w => w.Id);

                entity.Property(w => w.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
