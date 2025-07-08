using NewsPortal.Domain.Models;

namespace NewsPortal.Domain.Repositories;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetAll();
    public Task<Category> Create(CategoryDtoCreate category);
}