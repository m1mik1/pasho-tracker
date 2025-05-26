using PaSho_Tracker.Application.Services;
using PaSho_Tracker.Data;
using PaSho_Tracker.Domain.Model;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<TaskService> _logger;

    public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<TaskDto>> GetAll()
    {
        try
        {
            var tasks = await _taskRepository.GetAllAsync();
            List<TaskDto> taskDtos = new List<TaskDto>();
            foreach (var task in tasks)
            {
                var taskDto = new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Priority = task.Priority,
                    Deadline = task.Deadline,
                    AssignedUserId = task.AssignedUserId,
                    Status = task.Status
                };
                taskDtos.Add(taskDto);
            }

            _logger.LogInformation("Retrieving all tasks");
            return taskDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all tasks");
            return Enumerable.Empty<TaskDto>();
        }
    }

    public async Task<TaskDto?> Get(int id)
    {
        try
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                _logger.LogWarning("Failed to get task with ID: {ID}", id);
                return null;
            }

            var taskDto = new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Deadline = task.Deadline,
                AssignedUserId = task.AssignedUserId,
                Status = task.Status
            };
            _logger.LogInformation("Retrieving task with ID: {ID}", id);
            return taskDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting task");
            return null;
        }
    }

    public async Task<TaskDto?> Create(CreateTaskDto model)
    {
        try
        {
            var task = new TaskModel
            {
                Title = model.Title,
                Description = model.Description,
                Priority = model.Priority,
                Deadline = model.Deadline,
                AssignedUserId = model.AssignedUserId,
                Status = model.Status
            };
            var validationResults = ValidationService.Validate(task);
            if (validationResults.Any())
            {
                _logger.LogWarning("Validation failed: {Errors}",
                    string.Join(" | ", validationResults.Select(r => r.ErrorMessage)));
                return null;
            }
            await _taskRepository.AddAsync(task);
            
            var taskDto = new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Deadline = task.Deadline,
                AssignedUserId = task.AssignedUserId,
                Status = task.Status
            };
            _logger.LogInformation("Created new task with ID: {ID}", task.Id);
            return taskDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating task");
            return null;
        }
    }

    public async Task<bool> Update(UpdateTaskDto model)
    {
        try
        {
            var task = await _taskRepository.GetByIdAsync(model.Id);
            if (task == null)
            {
                _logger.LogWarning("Task with ID: {ID} not found.", model.Id);
                return false;
            }

            task.Title = model.Title;
            task.Description = model.Description;
            task.Priority = model.Priority;
            task.Deadline = model.Deadline;
            task.AssignedUserId = model.AssignedUserId;
            task.Status = model.Status;
            
            var validationResults = ValidationService.Validate(task);
            if (validationResults.Any())
            {
                _logger.LogWarning("Validation failed: {Errors}",
                    string.Join(" | ", validationResults.Select(r => r.ErrorMessage)));
                return false;
            }
            
            await _taskRepository.UpdateAsync(task);
            _logger.LogInformation("Updated task with ID: {ID}", task.Id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating task");
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                _logger.LogWarning("Task with ID: {ID} not found.", id);
                return false;
            }

            await _taskRepository.DeleteAsync(task);
            _logger.LogInformation("Deleted task with ID: {ID}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting task");
            return false;
        }
    }

    public async Task<IEnumerable<TaskDto>> GetSortedByDeadline()
    {
        try
        {
            var tasks = await _taskRepository.GetSortedByDeadlineAsync();
            List<TaskDto> taskDtos = new List<TaskDto>();
            foreach (var task in tasks)
            {
                var taskDto = new TaskDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    Priority = task.Priority,
                    Deadline = task.Deadline,
                    AssignedUserId = task.AssignedUserId,
                    Status = task.Status
                };
                taskDtos.Add(taskDto);
            }
            
            return taskDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sorting all tasks");
            return Enumerable.Empty<TaskDto>();
        }
    }
}