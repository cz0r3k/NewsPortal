using Microsoft.EntityFrameworkCore;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;
using NewsPortal.Infrastructure.Context;

namespace NewsPortal.Infrastructure.Repositories;

public class SlugRepository(NewsContext context) : ISlugRepository
{
    public async Task<int> CountSameSlug(string slug, CancellationToken cancellationToken)
    {
        return await context.Slugs.CountAsync(s => s.Slug == slug, cancellationToken);
    }

    public async Task<int> IncrementSlug(string slug)
    {
        var slugCounter = await context.Slugs.FindAsync(slug);
        if (slugCounter is not null)
        {
            slugCounter.Increment();
            context.Update(slugCounter);
            return slugCounter.Count;
        }

        context.Add(new SlugCounter { Slug = slug });
        return 0;
    }
}