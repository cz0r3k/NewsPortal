using NewsPortal.Domain.Models;

namespace NewsPortal.Domain.Repositories;

public interface IArticleRepository
{
    public Task<Article> Create(ArticleDtoCreate article);
    public Task<IEnumerable<Article>> GetAll();
    public Task<IEnumerable<Article>> GetByStatus(ArticleStatus status);
    public Task<Article> GetById(Guid id);
    public Task<Article> Update(Guid id, ArticleDtoUpdate article);
    public Task<Article> Publish(Guid id);
    public Task<int> CountSameSlug(string slug);
}