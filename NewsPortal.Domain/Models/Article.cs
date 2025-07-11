﻿using System.ComponentModel.DataAnnotations;

namespace NewsPortal.Domain.Models;

public class Article
{
    public const int ContentMinLength = 10;

    public Guid Id { get; set; } = Guid.NewGuid();
    [Required(AllowEmptyStrings = false)] public required string Title { get; set; }
    [MinLength(ContentMinLength)] public required string Content { get; set; }
    public required string Author { get; set; }
    public string Slug { get; set; } = string.Empty;
    public Guid? CategoryId { get; set; }
    public ArticleStatus Status { get; set; } = ArticleStatus.Draft;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public void Publish() => Status = ArticleStatus.Published;

    public static string GenerateSlug(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return string.Empty;

        return new string(title.ToLower()
            .Replace(" ", "-")
            .Where(c => char.IsLetterOrDigit(c) || c == '-')
            .ToArray());
    }
}