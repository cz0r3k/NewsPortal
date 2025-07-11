using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Features.ArticlesFeatures.Add;
using NewsPortal.Application.Features.ArticlesFeatures.Get;
using NewsPortal.Application.Features.ArticlesFeatures.Publish;
using NewsPortal.Application.Features.ArticlesFeatures.Update;
using NewsPortal.Application.Features.CategoriesFeatures.Get;
using NewsPortal.Domain.Models;
using NewsPortal.Domain.Services;

namespace NewsPortal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController(
    ILogger<CategoriesController> logger,
    IMediator mediator,
    IArticleStatsService articleStatsService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<IEnumerable<Article>>> GetAll([FromQuery] ArticleStatus? status,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllArticlesRequest(status), cancellationToken);
        return Ok(response.Value);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Article>> Add(AddArticleRequest request)
    {
        var response = await mediator.Send(request);
        if (response.IsSuccess)
            return Ok(response.Value);
        logger.LogError("Add article error [{}]", response.Errors[0].Message);
        return NotFound();
    }

    [HttpGet("{articleId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Article>> GetById(Guid articleId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetArticleByIdRequest(articleId), cancellationToken);
        if (response.IsSuccess)
        {
            return Ok(response.Value);
        }
        logger.LogError("Get article by Id error [{}]", response.Errors[0].Message);
        return NotFound();
    }

    [HttpPut("{articleId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Article>> Update(Guid articleId, UpdateArticle request)
    {
        var response = await mediator.Send(request.ToUpdateArticleRequest(articleId));
        if (response.IsSuccess)
        {
            return Ok(response.Value);
        }

        logger.LogError("Update article error [{}]", response.Errors[0].Message);
        return NotFound();
    }

    [HttpPost("{articleId:guid}/publish")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<Article>> Publish(Guid articleId)
    {
        var response = await mediator.Send(new PublishArticleRequest(articleId));
        if (response.IsSuccess)
        {
            return Ok(response.Value);
        }

        logger.LogError("Publish article error [{}]", response.Errors[0].Message);
        return NotFound();
    }

    [HttpGet("stats")]
    [ProducesResponseType(200)]
    public async Task<ActionResult<ArticleStats>> Stats()
    {
        var articlesResponse = await mediator.Send(new GetAllArticlesRequest());
        var articles = articlesResponse.Value;

        var published = articleStatsService.CountPublished(articles);
        var drafts = articleStatsService.CountDrafts(articles);
        var categoryId = articleStatsService.MostCommonlyUsedCategory(articles);

        var categoryName = await GetCategoryNameAsync(categoryId);
        return Ok(new ArticleStats(published, drafts, categoryName));
    }

    private async Task<string?> GetCategoryNameAsync(Guid? categoryId)
    {
        if (categoryId is null)
            return null;
        var categoryResponse = await mediator.Send(new GetCategoryByIdRequest(categoryId.Value));
        if (categoryResponse.IsSuccess)
            return categoryResponse.Value.Name;
        logger.LogError("Get category by Id error [{}]", categoryResponse.Errors[0].Message);
        return null;
    }
}