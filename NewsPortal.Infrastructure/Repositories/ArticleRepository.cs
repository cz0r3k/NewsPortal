using Microsoft.EntityFrameworkCore;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;
using NewsPortal.Infrastructure.Context;

namespace NewsPortal.Infrastructure.Repositories;

public class ArticleRepository(NewsContext context) : IArticleRepository
{
    public async Task<Article> Create(Article article)
    {
        await context.Articles.AddAsync(article);
        await context.SaveChangesAsync();
        return article;
    }

    public async Task<IEnumerable<Article>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Articles.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Article>> GetByStatus(ArticleStatus status, CancellationToken cancellationToken)
    {
        return await context.Articles.Where(a => a.Status == status).ToListAsync(cancellationToken);
    }

    public async Task<Article?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Articles.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Article?> Update(Guid id, Article article)
    {
        var existingArticle = await context.Articles.FirstOrDefaultAsync(a => a.Id == id);
    
        if (existingArticle == null)
            return null;
        
        existingArticle.Title = article.Title;
        existingArticle.Content = article.Content;
        existingArticle.Author = article.Author;
        existingArticle.Slug = article.Slug;
        existingArticle.CategoryId = article.CategoryId;
        existingArticle.Status = article.Status;

        await context.SaveChangesAsync();
        return existingArticle;

    }

    public async Task<Article?> Publish(Guid id)
    {
        var article = await context.Articles.FirstOrDefaultAsync(a => a.Id == id);
        article?.Publish();
        await context.SaveChangesAsync();
        return article;
    }

    public Task<int> CountSameSlug(string slug)
    {

        return Task.FromResult(0);
    }
}