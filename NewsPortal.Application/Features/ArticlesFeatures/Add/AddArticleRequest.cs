using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Add;

public sealed record AddArticleRequest(
    [Required(AllowEmptyStrings = false)] string Title,
    [MinLength(Article.ContentMinLength)] string Content,
    string Author,
    Guid? CategoryId = null,
    ArticleStatus Status = ArticleStatus.Draft) : IRequest<Result<Article>>;