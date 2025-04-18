using PaSho_Tracker.DTO;

namespace PaSho_Tracker.Interface;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAll();

    Task<TaskDto?> Get(int id);

    Task<TaskDto?> Create(CreateTaskDto model);

    Task<bool> Update(UpdateTaskDto model);

    Task<bool> Delete(int id);

    Task<IEnumerable<TaskDto>> GetSortedByDeadline();
}