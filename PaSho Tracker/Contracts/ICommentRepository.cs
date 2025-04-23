using PaSho_Tracker.Data;
using PaSho_Tracker.Domain.Model;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Interface;

public interface ICommentRepository : IRepository<CommentModel>
{
    Task<IEnumerable<CommentModel>> GetByTaskIdAsync(int taskId);
    Task<bool> ExistsAsync(int commentId);
    Task<TaskModel?> GetRelatedTaskAsync(int taskId);
}