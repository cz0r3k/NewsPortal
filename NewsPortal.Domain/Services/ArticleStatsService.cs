using NewsPortal.Domain.Models;

namespace NewsPortal.Domain.Services;

public class ArticleStatsService : IArticleStatsService
{
    public int CountPublished(IEnumerable<Article> articles) =>
        articles.Count(a => a.Status == ArticleStatus.Published);

    public int CountDrafts(IEnumerable<Article> articles) =>
        articles.Count(a => a.Status == ArticleStatus.Draft);

    public Guid? MostCommonlyUsedCategory(IEnumerable<Article> articles)
    {
        var grouped = articles
            .Where(a => a.CategoryId.HasValue)
            .GroupBy(a => a.CategoryId)
            .Select(g => new { CategoryId = g.Key, Count = g.Count() })
            .ToList();

        if (grouped.Count == 0)
            return null;

        var maxCount = grouped.Max(g => g.Count);
        var mostCommon = grouped.Where(g => g.Count == maxCount).ToList();

        return mostCommon.Count == 1 ? mostCommon[0].CategoryId : null;
    }
}