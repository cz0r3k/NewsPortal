using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Domain.Models;


public class Article
{
    private const int ContentMinLength = 10;
    
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required(AllowEmptyStrings = false)]
    public required string Title { get; set; }
    [MinLength(ContentMinLength)]
    public required string Content { get; set; }
    public required string Author { get; set; }
    public string Slug { get; set; }
    public ArticleStatus Status { get; set; } = ArticleStatus.Draft;
    public DateTime CreatedAt { get; } =  DateTime.Now;
}