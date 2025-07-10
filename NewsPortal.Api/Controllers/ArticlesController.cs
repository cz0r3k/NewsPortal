using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Features.ArticlesFeatures.Add;
using NewsPortal.Application.Features.ArticlesFeatures.Get;
using NewsPortal.Application.Features.ArticlesFeatures.Publish;
using NewsPortal.Application.Features.ArticlesFeatures.Update;
using NewsPortal.Domain.Models;

namespace NewsPortal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController(ILogger<CategoriesController> logger, IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetAll([FromQuery] ArticleStatus? status,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllArticlesRequest(status), cancellationToken);
        logger.LogInformation("Get all articles");
        return Ok(response.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Article>> Add(AddArticleRequest request)
    {
        var response = await mediator.Send(request);
        return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors[0].Message);
    }

    [HttpGet("{articleId:guid}")]
    public async Task<ActionResult<Article>> GetById(Guid articleId, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetArticleByIdRequest(articleId), cancellationToken);
        return response.IsSuccess ? Ok(response.Value) : NotFound();
    }

    [HttpPut("{articleId:guid}")]
    public async Task<ActionResult<Article>> Update(Guid articleId, UpdateArticle request)
    {
        var response = await mediator.Send(request.ToUpdateArticleRequest(articleId));
        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Errors[0].Message);
    }

    [HttpPost("{articleId:guid}/publish")]
    public async Task<ActionResult<Article>> Publish(Guid articleId)
    {
        var response = await mediator.Send(new PublishArticleRequest(articleId));
        return response.IsSuccess ? Ok(response.Value) : NotFound();
    }
}