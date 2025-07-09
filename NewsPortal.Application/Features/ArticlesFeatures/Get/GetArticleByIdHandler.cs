using FluentResults;
using MediatR;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Get;

public class GetArticleByIdHandler(IArticleRepository articleRepository)
    : IRequestHandler<GetArticleByIdRequest, Result<Article>>
{
    public async Task<Result<Article>> Handle(GetArticleByIdRequest request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetById(request.Id, cancellationToken);
        return article is null ? Result.Fail("Article not found") : Result.Ok(article);
    }
}