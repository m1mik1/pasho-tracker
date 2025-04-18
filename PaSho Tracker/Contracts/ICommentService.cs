using PaSho_Tracker.DTO;

namespace PaSho_Tracker.Interface;

public interface ICommentService
{
    Task<IEnumerable<CommentDto>> GetAll();
    Task<CommentDto?> Get(int id);
    Task<IEnumerable<CommentDto>> GetByTaskId(int taskId);
    Task<CommentDto?> Create(CreateCommentDto model);
    Task<bool> Update(UpdateCommentDto model);
    Task<bool> Delete(int id);
}