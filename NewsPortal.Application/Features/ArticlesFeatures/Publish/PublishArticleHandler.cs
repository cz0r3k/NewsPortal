using FluentResults;
using MediatR;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Publish;

public class PublishArticleHandler(IArticleRepository articleRepository)
    : IRequestHandler<PublishArticleRequest, Result<Article>>
{
    public async Task<Result<Article>> Handle(PublishArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.Publish(request.Id);
        return article is null ? Result.Fail("Article not found") : Result.Ok(article);
    }
}