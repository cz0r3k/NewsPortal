namespace NewsPortal.Application.Repositories;

public interface ISlugRepository
{
    public Task<int> CountSameSlug(string slug, CancellationToken cancellationToken);
    public Task<int> IncrementSlug(string slug);
}