using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PaSho_Tracker.Interface;
using PaSho_Tracker.Services;
using PaSho_Tracker.Domain.Model;

namespace PaSho_Tracker_Tests
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repo;
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _repo = new Mock<ITaskRepository>();
            // NullLogger<T> — простая «заглушка» для ILogger
            var logger = NullLogger<TaskService>.Instance;
            _service = new TaskService(_repo.Object, logger);
        }

        [Fact]
        public async Task DeleteNonExistingIdReturnsFalse()
        {
            // Arrange: репозиторий не найдёт задачу
            _repo
                .Setup(r => r.GetByIdAsync(42))
                .ReturnsAsync((TaskModel?)null);

            // Act
            var result = await _service.Delete(42);
            
            // Assert
            Assert.False(result);
            // Проверяем, что delete не вызывался, потому что не было объекта
            _repo.Verify(r => r.DeleteAsync(It.IsAny<TaskModel>()), Times.Never);
        }

        [Fact]
        public async Task DeleteExistingIdReturnsTrue()
        {
            // Arrange: репозиторий вернёт сущность, и удаление «успешно»
            var existing = new TaskModel { Id = 100, Title = "T", Description = "D" };
            _repo
                .Setup(r => r.GetByIdAsync(100))
                .ReturnsAsync(existing);
            _repo
                .Setup(r => r.DeleteAsync(existing))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.Delete(100);

            // Assert
            Assert.True(result);
            // Проверяем, что метод DeleteAsync был вызван именно с той сущностью
            _repo.Verify(r => r.DeleteAsync(existing), Times.Once);
        }
    }
}