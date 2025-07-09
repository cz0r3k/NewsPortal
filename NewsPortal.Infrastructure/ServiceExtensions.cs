using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsPortal.Application.Repositories;
using NewsPortal.Infrastructure.Context;
using NewsPortal.Infrastructure.Repositories;

namespace NewsPortal.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services)
    {
        services.AddDbContext<NewsContext>(options => options.UseInMemoryDatabase("NewsPortalInMemoryDb"));
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}