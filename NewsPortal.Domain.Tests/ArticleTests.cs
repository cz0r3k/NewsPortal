using System.ComponentModel.DataAnnotations;
using NewsPortal.Domain.Models;

namespace NewsPortal.Domain.Tests;

public class ArticleTests
{
    [Theory]
    [InlineData("Test Title", "test-title")]
    [InlineData("Test Title With Spaces", "test-title-with-spaces")]
    [InlineData("Test123 Title", "test123-title")]
    [InlineData("Test!@# Title$%^", "test-title")]
    [InlineData("UPPERCASE Title", "uppercase-title")]
    [InlineData("MiXeD CaSe Title", "mixed-case-title")]
    [InlineData("Test-Title-With-Hyphens", "test-title-with-hyphens")]
    [InlineData("Test_Title_With_Underscores", "testtitlewithunderscores")]
    [InlineData("Żółć Gęślą Jaźń", "żółć-gęślą-jaźń")]
    public void GenerateSlug_ShouldReturnCorrectSlug(string title, string expectedSlug)
    {
        // Act
        var result = Article.GenerateSlug(title);

        // Assert
        Assert.Equal(expectedSlug, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\n")]
    public void GenerateSlug_WithEmptyOrWhitespaceTitle_ShouldReturnEmptyString(string title)
    {
        // Act
        var result = Article.GenerateSlug(title);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Article_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange
        var title = "Test Article";
        var content = "This is a test article content with more than 10 characters.";
        var author = "Test Author";

        // Act
        var article = new Article
        {
            Title = title,
            Content = content,
            Author = author
        };

        // Assert
        Assert.Equal(title, article.Title);
        Assert.Equal(content, article.Content);
        Assert.Equal(author, article.Author);
        Assert.NotEqual(Guid.Empty, article.Id);
        Assert.Equal(ArticleStatus.Draft, article.Status);
        Assert.True(article.CreatedAt <= DateTime.Now);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t\n")]
    public void Article_WithEmptyTitle_ShouldFailValidation(string invalidTitle)
    {
        // Arrange
        var article = new Article
        {
            Title = invalidTitle,
            Content = "This is a test article content with more than 10 characters.",
            Author = "Test Author"
        };

        // Act
        var validationResults = ValidateModel(article);

        // Assert
        Assert.Contains(validationResults, v => v.MemberNames.Contains("Title"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("Short")]
    [InlineData("123456789")] // Exactly 9 characters
    public void Article_WithTooShortContent_ShouldFailValidation(string invalidContent)
    {
        // Arrange
        var article = new Article
        {
            Title = "Test Title",
            Content = invalidContent,
            Author = "Test Author"
        };

        // Act
        var validationResults = ValidateModel(article);

        // Assert
        Assert.Contains(validationResults, v => v.MemberNames.Contains("Content"));
    }

    [Fact]
    public void Article_WithValidContent_ShouldPassValidation()
    {
        // Arrange
        var article = new Article
        {
            Title = "Test Title",
            Content = "1234567890", // Exactly 10 characters
            Author = "Test Author"
        };

        // Act
        var validationResults = ValidateModel(article);

        // Assert
        Assert.DoesNotContain(validationResults, v => v.MemberNames.Contains("Content"));
    }

    [Fact]
    public void Publish_ShouldChangeStatusToPublished()
    {
        // Arrange
        var article = new Article
        {
            Title = "Test Title",
            Content = "This is a test article content with more than 10 characters.",
            Author = "Test Author"
        };

        // Act
        article.Publish();

        // Assert
        Assert.Equal(ArticleStatus.Published, article.Status);
    }

    private static List<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, context, validationResults, true);
        return validationResults;
    }
}