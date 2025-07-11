using FluentResults;
using MediatR;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Add;

public class AddArticleHandler(
    IArticleRepository articleRepository,
    ISlugRepository slugRepository,
    ICategoryRepository categoryRepository)
    : IRequestHandler<AddArticleRequest, Result<Article>>
{
    public async Task<Result<Article>> Handle(AddArticleRequest request, CancellationToken cancellationToken)
    {
        if (request.CategoryId is not null && await categoryRepository.Exists(request.CategoryId.Value) is false)
        {
            return Result.Fail($"Category (id:{request.CategoryId}) not found");
        }

        var slug = Article.GenerateSlug(request.Title);
        var slugNumber = await slugRepository.IncrementSlug(slug);
        var finalSlug = slugNumber == 0 ? slug : $"{slug}-{slugNumber}";
        var article = new Article
        {
            Author = request.Author,
            Slug = finalSlug,
            Content = request.Content,
            Title = request.Title,
            CategoryId = request.CategoryId,
            Status = request.Status
        };
        await articleRepository.Create(article);
        return Result.Ok(article);
    }
}