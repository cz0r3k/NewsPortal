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
    public async Task<ActionResult<IEnumerable<Category>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await mediator.Send(new GetAllCategoriesRequest(), cancellationToken);
        return Ok(response.Value);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> Add(AddCategoryRequest request, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(request, cancellationToken);
        return response.IsFailed ? BadRequest(response.Errors) : Ok(response.Value);
    }
}