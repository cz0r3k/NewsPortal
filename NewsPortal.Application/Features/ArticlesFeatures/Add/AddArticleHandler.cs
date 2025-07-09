using FluentResults;
using MediatR;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Add;

public class AddArticleHandler(IArticleRepository articleRepository)
    : IRequestHandler<AddArticleRequest, Result<Article>>
{
    public async Task<Result<Article>> Handle(AddArticleRequest request, CancellationToken cancellationToken)
    {
        var slug = Article.GenerateSlug(request.Title);
        var count = await articleRepository.CountSameSlug(slug);
        var finalSlug = count == 0 ? slug : $"{slug}-{count}";
        var article = new Article
        {
            Author = request.Author, Slug = finalSlug, Content = request.Content, Title = request.Title,
            Status = request.Status
        };
        await articleRepository.Create(article);
        return Result.Ok(article);
    }
}