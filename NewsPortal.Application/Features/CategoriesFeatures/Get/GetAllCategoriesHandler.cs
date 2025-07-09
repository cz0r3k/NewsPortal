using FluentResults;
using MediatR;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.CategoriesFeatures.Get;

public sealed class GetAllCategoriesHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetAllCategoriesRequest, Result<List<Category>>>
{
    public async Task<Result<List<Category>>> Handle(GetAllCategoriesRequest request,
        CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAll(cancellationToken);
        return Result.Ok(categories.ToList());
    }
}