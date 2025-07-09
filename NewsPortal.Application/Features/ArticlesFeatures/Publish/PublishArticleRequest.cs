using FluentResults;
using MediatR;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Publish;

public sealed record PublishArticleRequest(Guid Id) : IRequest<Result<Article>>;