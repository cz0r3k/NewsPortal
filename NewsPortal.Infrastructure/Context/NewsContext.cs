using Microsoft.EntityFrameworkCore;
using NewsPortal.Domain.Models;

namespace NewsPortal.Infrastructure.Context;

public class NewsContext(DbContextOptions<NewsContext> options) : DbContext(options)
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SlugCounter> Slugs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>(entity => { entity.HasIndex(e => e.Name).IsUnique(); });
    }
}