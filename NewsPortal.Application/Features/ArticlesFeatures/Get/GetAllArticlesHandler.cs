using FluentResults;
using MediatR;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Get;

public class GetAllArticlesHandler(IArticleRepository articleRepository)
    : IRequestHandler<GetAllArticlesRequest, Result<List<Article>>>
{
    public async Task<Result<List<Article>>> Handle(GetAllArticlesRequest request, CancellationToken cancellationToken)
    {
        IEnumerable<Article> articles;
        if (request.Status.HasValue)
        {
            articles = await articleRepository.GetByStatus(request.Status.Value, cancellationToken);
        }
        else
        {
            articles = await articleRepository.GetAll(cancellationToken);
        }

        return Result.Ok(articles.ToList());
    }
}