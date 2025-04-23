using Microsoft.EntityFrameworkCore;
using PaSho_Tracker.Domain.Model;
using PaSho_Tracker.Interface;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Data
{
    public class CommentRepository : BaseRepository<CommentModel>, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context) { }
        
        public async Task<IEnumerable<CommentModel>> GetByTaskIdAsync(int taskId)
        {
            return await _context.Comments
                .Where(c => c.RelatedTaskId == taskId)
                .ToListAsync();
        }
        
        public async Task<bool> ExistsAsync(int commentId)
        {
            return await _context.Comments.AnyAsync(c => c.Id == commentId);
        }
        
        public async Task<TaskModel?> GetRelatedTaskAsync(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId);
        }

    }
}