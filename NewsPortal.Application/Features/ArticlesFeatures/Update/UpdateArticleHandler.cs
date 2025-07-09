using FluentResults;
using MediatR;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Update;

public class UpdateArticleHandler(IArticleRepository articleRepository)
    : IRequestHandler<UpdateArticleRequest, Result<Article>>
{
    public async Task<Result<Article>> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
    {
        var article = await articleRepository.GetById(request.Id);
        if (article is null)
            return Result.Fail("Article not found");
        if (request.Title is not null)
        {
            article.Title = request.Title;
            var oldSlug = Article.GenerateSlug(article.Title);
            var newSlug = Article.GenerateSlug(request.Title);
            if (!Equals(oldSlug, newSlug))
            {
                var count = await articleRepository.CountSameSlug(newSlug);
                var finalSlug = count == 0 ? newSlug : $"{newSlug}-{count}";
                article.Slug = finalSlug;
            }
        }

        if (request.Content is not null)
            article.Content = request.Content;
        if (request.Author is not null)
            article.Author = request.Author;
        if (request.Status is not null)
            article.Status = (ArticleStatus)request.Status;
        var updatedArticle = await articleRepository.Update(request.Id, article);
        return Result.Ok(updatedArticle!);
    }
}