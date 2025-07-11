namespace NewsPortal.Domain.Models;

public class ArticleStats(int published, int drafts, string? mostCommonlyUsedCategory)
{
    public int Published { get; set; } = published;
    public int Drafts { get; set; } = drafts;
    public string? MostCommonlyUsedCategory { get; set; } = mostCommonlyUsedCategory;
}