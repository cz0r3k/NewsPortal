using NewsPortal.Domain.Models;

namespace NewsPortal.Application.Features.ArticlesFeatures.Update;


public class UpdateArticle
{
    public string? Title{ get; set; }
    public string? Content{ get; set; }
    public string? Author{ get; set; }
    public ArticleStatus? Status{ get; set; }
    
    public UpdateArticleRequest ToUpdateArticleRequest(Guid id)
    {
        return new UpdateArticleRequest(id, Title, Content, Author, Status);
    }
}