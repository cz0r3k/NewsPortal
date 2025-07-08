using NewsPortal.Domain.Models;

namespace NewsPortal.Domain.Services;

public class ArticleStatsService
{
    public int CountPublished(IEnumerable<Article> articles) => 
        articles.Count(a => a.Status == ArticleStatus.Published);

    public int CountDrafts(IEnumerable<Article> articles) => 
        articles.Count(a => a.Status == ArticleStatus.Draft);
}