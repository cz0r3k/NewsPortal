using FluentResults;
using MediatR;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.CategoriesFeatures.Get;

public record GetCategoryByIdRequest(Guid Id) : IRequest<Result<Category>>;