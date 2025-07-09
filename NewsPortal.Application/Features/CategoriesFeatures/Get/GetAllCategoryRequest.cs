using FluentResults;
using MediatR;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.CategoriesFeatures.Get;

public sealed record GetAllCategoriesRequest : IRequest<Result<List<Category>>>;