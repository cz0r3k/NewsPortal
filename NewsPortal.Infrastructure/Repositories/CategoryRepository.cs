using Microsoft.EntityFrameworkCore;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;
using NewsPortal.Infrastructure.Context;

namespace NewsPortal.Infrastructure.Repositories;

public class CategoryRepository(NewsContext context) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Categories.ToListAsync(cancellationToken);
    }

    public async Task<Category> Create(Category category)
    {
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<Category?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await context.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await context.Categories.AnyAsync(c => c.Id == id);
    }
}