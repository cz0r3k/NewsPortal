using FluentResults;
using MediatR;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Add;

public sealed record AddArticleRequest(
    string Title,
    string Content,
    string Author,
    ArticleStatus Status = ArticleStatus.Draft) : IRequest<Result<Article>>;