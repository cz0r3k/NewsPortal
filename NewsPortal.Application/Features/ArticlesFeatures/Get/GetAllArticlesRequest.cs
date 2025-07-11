using FluentResults;
using MediatR;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Get;

public sealed record GetAllArticlesRequest(ArticleStatus? Status = null) : IRequest<Result<List<Article>>>;