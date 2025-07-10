using FluentResults;
using MediatR;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Update;

public record UpdateArticleRequest(
    Guid Id,
    string? Title,
    string? Content,
    string? Author,
    Guid? CategoryId,
    ArticleStatus? Status) : IRequest<Result<Article>>;