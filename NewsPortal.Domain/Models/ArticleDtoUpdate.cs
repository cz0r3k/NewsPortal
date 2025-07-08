namespace NewsPortal.Domain.Models;

public class ArticleDtoUpdate
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string Author { get; set; }
    public ArticleStatus Status { get; set; }
}