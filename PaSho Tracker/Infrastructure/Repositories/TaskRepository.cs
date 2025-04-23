using Microsoft.EntityFrameworkCore;
using PaSho_Tracker.Domain.Model;
using PaSho_Tracker.Interface;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Data
{
    public class TaskRepository : BaseRepository<TaskModel>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<TaskModel>> GetSortedByDeadlineAsync()
        {
            return await _context.Tasks.OrderBy(t => t.Deadline).ToListAsync();
        }

        public async Task<bool> ExistsAsync(int taskId)
        {
            return await _context.Tasks.AnyAsync(t => t.Id == taskId);
        }
    }
}