using PaSho_Tracker.Data;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Interface;

public interface ITaskRepository : IRepository<TaskModel>
{
    Task<IEnumerable<TaskModel>> GetSortedByDeadlineAsync();
    Task<bool> ExistsAsync(int taskId);
}