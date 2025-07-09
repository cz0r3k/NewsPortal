using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Domain.Models;

public class Article
{
    private const int ContentMinLength = 10;

    public Guid Id { get; set; } = Guid.NewGuid();
    [Required(AllowEmptyStrings = false)] public required string Title { get; set; }
    [MinLength(ContentMinLength)] public required string Content { get; set; }
    public required string Author { get; set; }
    public string Slug { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; } = null;
    public ArticleStatus Status { get; set; } = ArticleStatus.Draft;
    public DateTime CreatedAt { get; } = DateTime.Now;

    public void Publish() => Status = ArticleStatus.Published;

    public static string GenerateSlug(string title) => string.Empty +
                                                       title.ToLower().Replace(" ", "-").Where(c =>
                                                           char.IsLetterOrDigit(c) || c is '-');
}