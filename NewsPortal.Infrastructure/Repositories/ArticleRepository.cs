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
        return await context.Articles.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public Task<Article?> Update(Article article)
    {
        context.Update(article);
        return Task.FromResult(article)!;
    }

    public async Task<Article?> Publish(Guid id)
    {
        var article = await context.Articles.FirstOrDefaultAsync(a => a.Id == id);
        article?.Publish();
        await context.SaveChangesAsync();
        return article;
    }
}