using FluentAssertions;
using NewsPortal.Domain.Models;
using NewsPortal.Domain.Services;

namespace NewsPortal.Domain.Tests;

public class ArticleStatsServiceTests
{
    private readonly ArticleStatsService _articleStatsService = new();

    [Fact]
    public void CountPublished_WithPublishedArticles_ShouldReturnCorrectCount()
    {
        // Arrange
        var articles = new List<Article>
        {
            new() { Title = "Article 1", Content = "Content 1", Author = "Author 1", Status = ArticleStatus.Published },
            new() { Title = "Article 2", Content = "Content 2", Author = "Author 2", Status = ArticleStatus.Published },
            new() { Title = "Article 3", Content = "Content 3", Author = "Author 3", Status = ArticleStatus.Draft },
            new() { Title = "Article 4", Content = "Content 4", Author = "Author 4", Status = ArticleStatus.Published }
        };

        // Act
        var result = _articleStatsService.CountPublished(articles);

        // Assert
        result.Should().Be(3);
    }

    [Fact]
    public void CountPublished_WithNoPublishedArticles_ShouldReturnZero()
    {
        // Arrange
        var articles = new List<Article>
        {
            new() { Title = "Draft 1", Content = "Content 1", Author = "Author 1", Status = ArticleStatus.Draft },
            new() { Title = "Draft 2", Content = "Content 2", Author = "Author 2", Status = ArticleStatus.Draft }
        };

        // Act
        var result = _articleStatsService.CountPublished(articles);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void CountPublished_WithEmptyList_ShouldReturnZero()
    {
        // Arrange
        var articles = new List<Article>();

        // Act
        var result = _articleStatsService.CountPublished(articles);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void CountDrafts_WithDraftArticles_ShouldReturnCorrectCount()
    {
        // Arrange
        var articles = new List<Article>
        {
            new() { Title = "Draft 1", Content = "Content 1", Author = "Author 1", Status = ArticleStatus.Draft },
            new()
            {
                Title = "Published 1", Content = "Content 2", Author = "Author 2", Status = ArticleStatus.Published
            },
            new() { Title = "Draft 2", Content = "Content 3", Author = "Author 3", Status = ArticleStatus.Draft },
            new() { Title = "Draft 3", Content = "Content 4", Author = "Author 4", Status = ArticleStatus.Draft }
        };

        // Act
        var result = _articleStatsService.CountDrafts(articles);

        // Assert
        result.Should().Be(3);
    }

    [Fact]
    public void CountDrafts_WithNoDraftArticles_ShouldReturnZero()
    {
        // Arrange
        var articles = new List<Article>
        {
            new()
            {
                Title = "Published 1", Content = "Content 1", Author = "Author 1", Status = ArticleStatus.Published
            },
            new()
            {
                Title = "Published 2", Content = "Content 2", Author = "Author 2", Status = ArticleStatus.Published
            }
        };

        // Act
        var result = _articleStatsService.CountDrafts(articles);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void MostCommonlyUsedCategory_WithSingleMostCommonCategory_ShouldReturnCategoryId()
    {
        // Arrange
        var categoryId1 = Guid.NewGuid();
        var categoryId2 = Guid.NewGuid();
        var articles = new List<Article>
        {
            new() { Title = "Tech 1", Content = "Content 1", Author = "Author 1", CategoryId = categoryId1 },
            new() { Title = "Tech 2", Content = "Content 2", Author = "Author 2", CategoryId = categoryId1 },
            new() { Title = "Tech 3", Content = "Content 3", Author = "Author 3", CategoryId = categoryId1 },
            new() { Title = "Sports 1", Content = "Content 4", Author = "Author 4", CategoryId = categoryId2 },
            new() { Title = "Sports 2", Content = "Content 5", Author = "Author 5", CategoryId = categoryId2 }
        };

        // Act
        var result = _articleStatsService.MostCommonlyUsedCategory(articles);

        // Assert
        result.Should().Be(categoryId1);
    }

    [Fact]
    public void MostCommonlyUsedCategory_WithTiedCategories_ShouldReturnNull()
    {
        // Arrange
        var categoryId1 = Guid.NewGuid();
        var categoryId2 = Guid.NewGuid();
        var articles = new List<Article>
        {
            new() { Title = "Tech 1", Content = "Content 1", Author = "Author 1", CategoryId = categoryId1 },
            new() { Title = "Tech 2", Content = "Content 2", Author = "Author 2", CategoryId = categoryId1 },
            new() { Title = "Sports 1", Content = "Content 3", Author = "Author 3", CategoryId = categoryId2 },
            new() { Title = "Sports 2", Content = "Content 4", Author = "Author 4", CategoryId = categoryId2 }
        };

        // Act
        var result = _articleStatsService.MostCommonlyUsedCategory(articles);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void MostCommonlyUsedCategory_WithNoCategorizedArticles_ShouldReturnNull()
    {
        // Arrange
        var articles = new List<Article>
        {
            new() { Title = "Uncategorized 1", Content = "Content 1", Author = "Author 1", CategoryId = null },
            new() { Title = "Uncategorized 2", Content = "Content 2", Author = "Author 2", CategoryId = null }
        };

        // Act
        var result = _articleStatsService.MostCommonlyUsedCategory(articles);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void MostCommonlyUsedCategory_WithEmptyList_ShouldReturnNull()
    {
        // Arrange
        var articles = new List<Article>();

        // Act
        var result = _articleStatsService.MostCommonlyUsedCategory(articles);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void MostCommonlyUsedCategory_WithMixedCategorizedAndUncategorizedArticles_ShouldReturnMostCommon()
    {
        // Arrange
        var categoryId1 = Guid.NewGuid();
        var categoryId2 = Guid.NewGuid();
        var articles = new List<Article>
        {
            new() { Title = "Tech 1", Content = "Content 1", Author = "Author 1", CategoryId = categoryId1 },
            new() { Title = "Tech 2", Content = "Content 2", Author = "Author 2", CategoryId = categoryId1 },
            new() { Title = "Sports 1", Content = "Content 3", Author = "Author 3", CategoryId = categoryId2 },
            new() { Title = "Uncategorized 1", Content = "Content 4", Author = "Author 4", CategoryId = null },
            new() { Title = "Uncategorized 2", Content = "Content 5", Author = "Author 5", CategoryId = null }
        };

        // Act
        var result = _articleStatsService.MostCommonlyUsedCategory(articles);

        // Assert
        result.Should().Be(categoryId1);
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(10, 7, 3)]
    [InlineData(1, 1, 0)]
    [InlineData(1, 0, 1)]
    public void CountMethods_WithVariousArticleCounts_ShouldReturnCorrectValues(
        int totalArticles, int publishedCount, int draftCount)
    {
        // Arrange
        var articles = new List<Article>();

        for (var i = 0; i < publishedCount; i++)
        {
            articles.Add(new Article
            {
                Title = $"Published Article {i + 1}",
                Content = $"Content {i + 1}",
                Author = $"Author {i + 1}",
                Status = ArticleStatus.Published
            });
        }

        for (var i = 0; i < draftCount; i++)
        {
            articles.Add(new Article
            {
                Title = $"Draft Article {i + 1}",
                Content = $"Draft Content {i + 1}",
                Author = $"Draft Author {i + 1}",
                Status = ArticleStatus.Draft
            });
        }

        // Act
        var publishedResult = _articleStatsService.CountPublished(articles);
        var draftResult = _articleStatsService.CountDrafts(articles);

        // Assert
        publishedResult.Should().Be(publishedCount);
        draftResult.Should().Be(draftCount);
        (publishedResult + draftResult).Should().Be(totalArticles);
    }
}