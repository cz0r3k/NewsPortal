using FluentResults;
using MediatR;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Update;

public class UpdateArticleHandler(
    IArticleRepository articleRepository,
    ISlugRepository slugRepository,
    ICategoryRepository categoryRepository)
    : IRequestHandler<UpdateArticleRequest, Result<Article>>
{
    public async Task<Result<Article>> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetById(request.Id, cancellationToken);
        if (article is null)
            return Result.Fail($"Article (id:{request.Id}) not found");

        if (request.CategoryId is not null && await categoryRepository.Exists(request.CategoryId.Value) is false)
        {
            return Result.Fail($"Category (id:{request.CategoryId}) not found");
        }

        if (request.Title is not null)
        {
            var oldSlug = Article.GenerateSlug(article.Title);
            var newSlug = Article.GenerateSlug(request.Title);
            if (!Equals(oldSlug, newSlug))
            {
                var slugNumber = await slugRepository.IncrementSlug(newSlug);
                var finalSlug = slugNumber == 0 ? newSlug : $"{newSlug}-{slugNumber}";
                article.Slug = finalSlug;
            }

            article.Title = request.Title;
        }

        if (request.Content is not null)
            article.Content = request.Content;
        if (request.Author is not null)
            article.Author = request.Author;
        if (request.CategoryId is not null)
            article.CategoryId = request.CategoryId;
        if (request.Status is not null)
            article.Status = (ArticleStatus)request.Status;
        var updatedArticle = await articleRepository.Update(article);
        return Result.Ok(updatedArticle!);
    }
}