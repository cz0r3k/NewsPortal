using NewsPortal.Application.Features.CategoriesFeatures.Add;
using NewsPortal.Application.Repositories;
using NewsPortal.Domain.Models;
using FluentAssertions;
using Moq;

namespace NewsPortal.Application.Tests;

public class AddCategoryHandlerTests
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly AddCategoryHandler _handler;

    public AddCategoryHandlerTests()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _handler = new AddCategoryHandler(_categoryRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenCategoryNameIsUnique_ShouldCreateCategory()
    {
        // Arrange
        var request = new AddCategoryRequest("Technology");
        var cancellationToken = CancellationToken.None;

        _categoryRepositoryMock
            .Setup(x => x.GetByName(request.Name, cancellationToken))
            .ReturnsAsync((Category?)null);

        _categoryRepositoryMock
            .Setup(x => x.Create(It.IsAny<Category>()))
            .ReturnsAsync((Category category) => category);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Name.Should().Be("Technology");

        _categoryRepositoryMock.Verify(x => x.GetByName(request.Name, cancellationToken), Times.Once);
        _categoryRepositoryMock.Verify(x => x.Create(It.Is<Category>(c => c.Name == request.Name)), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenCategoryNameAlreadyExists_ShouldReturnFailure()
    {
        // Arrange
        var request = new AddCategoryRequest("Technology");
        var cancellationToken = CancellationToken.None;
        var existingCategory = new Category { Name = "Technology" };

        _categoryRepositoryMock
            .Setup(x => x.GetByName(request.Name, cancellationToken))
            .ReturnsAsync(existingCategory);

        // Act
        var result = await _handler.Handle(request, cancellationToken);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().Contain(error => error.Message == $"Category with this name ({request.Name}) already exists");

        _categoryRepositoryMock.Verify(x => x.GetByName(request.Name, cancellationToken), Times.Once);
        _categoryRepositoryMock.Verify(x => x.Create(It.IsAny<Category>()), Times.Never);
    }
}