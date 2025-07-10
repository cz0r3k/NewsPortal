using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Repositories;

public interface IArticleRepository
{
    public Task<Article> Create(Article article);
    public Task<IEnumerable<Article>> GetAll(CancellationToken cancellationToken);
    public Task<IEnumerable<Article>> GetByStatus(ArticleStatus status, CancellationToken cancellationToken);
    public Task<Article?> GetById(Guid id, CancellationToken cancellationToken);
    public Task<Article?> Update(Article article);
    public Task<Article?> Publish(Guid id);
}