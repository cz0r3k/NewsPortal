using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Models;

namespace NewsPortal.Infrastructure.Context;

public class NewsContext(DbContextOptions<NewsContext> options) : DbContext(options)
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Slug).HasMaxLength(250);
            entity.HasIndex(e => e.Slug).IsUnique();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
        });
    }
}