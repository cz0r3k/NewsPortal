using FluentResults;
using MediatR;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.CategoriesFeatures.Get;

public class GetCategoryByIdHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetCategoryByIdRequest, Result<Category>>
{
    public async Task<Result<Category>> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetById(request.Id, cancellationToken);
        return category is null ? Result.Fail($"Category (id:{request.Id}) not found") : Result.Ok(category);
    }
}