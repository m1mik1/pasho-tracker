using System.Threading.Tasks;
using Moq;
using Xunit;
using PaSho_Tracker.Services;
using PaSho_Tracker.Interface;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Domain.Model;
using Microsoft.Extensions.Logging;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _repoMock;
    private readonly CategoryService _service;

    public CategoryServiceTests()
    {
        _repoMock = new Mock<ICategoryRepository>();
        var logger = Mock.Of<ILogger<CategoryService>>();
        _service   = new CategoryService(_repoMock.Object, logger);
    }

    [Fact]
    public async Task CreateDuplicateNullResult()
    {
        // Arrange
        var dto = new CreateCategoryDto { CategoryName = "Test" };
        _repoMock.Setup(r => r.ExistsByName("Test"))
            .ReturnsAsync(true);

        // Act
        var result = await _service.Create(dto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateValidModel()
    {
        // Arrange
        var dto = new CreateCategoryDto { CategoryName = "NewCat" };
        _repoMock.Setup(r => r.ExistsByName("NewCat"))
            .ReturnsAsync(false);
        _repoMock.Setup(r => r.AddAsync(It.IsAny<CategoryModel>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.Create(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("NewCat", result.CategoryName);
        _repoMock.Verify(r => r.AddAsync(It.Is<CategoryModel>(c => c.CategoryName == "NewCat")), Times.Once);
    }
}