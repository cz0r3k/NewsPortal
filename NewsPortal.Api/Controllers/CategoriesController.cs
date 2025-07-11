using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Features.CategoriesFeatures.Add;
using NewsPortal.Application.Features.CategoriesFeatures.Get;
using NewsPortal.Domain.Models;

namespace NewsPortal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ILogger<CategoriesController> logger, IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<IEnumerable<Category>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllCategoriesRequest(), cancellationToken);
        return Ok(response.Value);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Category>> Add(AddCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        if (response.IsSuccess)
        {
            var category = response.Value;
            logger.LogInformation("Add category success [category: {}]", category.Name);
            return Ok(category);
        }

        logger.LogError("Add category error [{}]", response.Errors[0].Message);
        return BadRequest();
    }
}