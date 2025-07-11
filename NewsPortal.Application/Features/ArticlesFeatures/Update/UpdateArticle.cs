using System.ComponentModel.DataAnnotations;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Update;

public class UpdateArticle : IValidatableObject
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Author { get; set; }
    public Guid? CategoryId { get; set; }
    public ArticleStatus? Status { get; set; }

    public UpdateArticleRequest ToUpdateArticleRequest(Guid id)
    {
        return new UpdateArticleRequest(id, Title, Content, Author, CategoryId, Status);
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Title is not null && string.IsNullOrWhiteSpace(Title))
        {
            yield return new ValidationResult(
                "Title cannot be an empty string when provided.",
                [nameof(Title)]);
        }

        if (Content is not null && Content.Length < Article.ContentMinLength)
        {
            yield return new ValidationResult(
                $"Content must be at least {Article.ContentMinLength} characters long when provided.",
                [nameof(Content)]);
        }
    }
}