using NewsPortal.Domain.Models;

namespace NewsPortal.Domain.Services;

public interface IArticleStatsService
{
    public int CountPublished(IEnumerable<Article> articles);
    public int CountDrafts(IEnumerable<Article> articles);
    public Guid? MostCommonlyUsedCategory(IEnumerable<Article> articles);
}