using FluentResults;
using MediatR;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.CategoriesFeatures.Add;

public sealed record AddCategoryRequest(string Name) : IRequest<Result<Category>>;