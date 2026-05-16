using Microsoft.EntityFrameworkCore;
using SchoolLibraryApp.Models;

namespace SchoolLibraryApp.Data;

/// <summary>
/// Контекст базы данных школьной библиотеки.
/// </summary>
public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Reader> Readers => Set<Reader>();
    public DbSet<LibraryCard> LibraryCards => Set<LibraryCard>();
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Author).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Isbn).HasMaxLength(32);
            entity.Property(x => x.Genre).HasMaxLength(100);
            entity.HasMany(x => x.Categories)
                .WithMany(x => x.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookCategories",
                    right => right.HasOne<Category>().WithMany().HasForeignKey("CategoryId"),
                    left => left.HasOne<Book>().WithMany().HasForeignKey("BookId"),
                    join =>
                    {
                        join.HasKey("BookId", "CategoryId");
                        join.ToTable("BookCategories");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
            entity.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.Property(x => x.FullName).HasMaxLength(200).IsRequired();
            entity.Property(x => x.ClassName).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(150);
            entity.HasOne(x => x.Card)
                .WithOne(x => x.Reader)
                .HasForeignKey<LibraryCard>(x => x.ReaderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<LibraryCard>(entity =>
        {
            entity.Property(x => x.Number).HasMaxLength(32).IsRequired();
            entity.HasIndex(x => x.Number).IsUnique();
        });

        modelBuilder.Entity<Loan>(entity =>
        {
            entity.HasOne(x => x.Book)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.BookId);
            entity.HasOne(x => x.Reader)
                .WithMany(x => x.Loans)
                .HasForeignKey(x => x.ReaderId);
        });
    }
}
