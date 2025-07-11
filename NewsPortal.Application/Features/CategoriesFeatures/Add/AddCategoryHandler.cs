using FluentResults;
using MediatR;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.CategoriesFeatures.Add;

public sealed class AddCategoryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<AddCategoryRequest, Result<Category>>
{
    public async Task<Result<Category>> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
    {
        if (await categoryRepository.GetByName(request.Name, cancellationToken) != null)
        {
            return Result.Fail($"Category with this name ({request.Name}) already exists");
        }

        var category = new Category { Name = request.Name };
        await categoryRepository.Create(category);
        return Result.Ok(category);
    }
}