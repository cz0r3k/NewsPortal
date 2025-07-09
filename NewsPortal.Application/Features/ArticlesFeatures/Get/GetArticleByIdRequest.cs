using FluentResults;
using MediatR;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Get;

public record GetArticleByIdRequest(Guid Id) : IRequest<Result<Article>>;