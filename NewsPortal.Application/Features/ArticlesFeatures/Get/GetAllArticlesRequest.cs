using FluentResults;
using MediatR;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Get;

public sealed record GetAllArticlesRequest(ArticleStatus? Status) : IRequest<Result<List<Article>>>;