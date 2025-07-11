using NewsPortal.Application.Features.CategoriesFeatures.Add;
using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Repositories;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken);
    public Task<Category> Create(Category category);
    public Task<Category?> GetByName(string name, CancellationToken cancellationToken);
    public Task<Category?> GetById(Guid id, CancellationToken cancellationToken);
    public Task<bool> Exists(Guid id);
}