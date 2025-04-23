using PaSho_Tracker.Application.Services;
using PaSho_Tracker.Data;
using PaSho_Tracker.DTO;
using PaSho_Tracker.Interface;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly ILogger<CommentService> _logger;

    public CommentService(ICommentRepository commentRepository, ILogger<CommentService> logger)
    {
        _commentRepository = commentRepository;
        _logger = logger;
    }
    

    public async Task<IEnumerable<CommentDto>> GetAll()
    {
        try
        {
            var comments = await _commentRepository.GetAllAsync();
            List<CommentDto> commentsDtoList = new List<CommentDto>();
            foreach (var comment in comments)
            {
                var commentDto = new CommentDto
                {
                    Id = comment.Id,
                    CommentText = comment.CommentText,
                    Author = comment.Author,
                    RelatedTaskId = comment.RelatedTaskId
                };
                commentsDtoList.Add(commentDto);
            }

            return commentsDtoList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all comments");
            return Enumerable.Empty<CommentDto>();
        }
    }

    public async Task<CommentDto?> Get(int id)
    {
        try
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                _logger.LogWarning("Comment with id: {Id} not found", id);
                return null;
            }

            var commentDto = new CommentDto
            {
                Id = comment.Id,
                CommentText = comment.CommentText,
                Author = comment.Author,
                RelatedTaskId = comment.RelatedTaskId
            };
            _logger.LogInformation("Found comment with id: {Id}", id);
            return commentDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comment");
            return null;
        }
    }

    public async Task<IEnumerable<CommentDto>> GetByTaskId(int taskId)
    {
        try
        {
            var comments = (await _commentRepository.GetByTaskIdAsync(taskId)).ToList();
            if (!comments.Any())
            {
                _logger.LogWarning("Comments with related TaskId: {Id} not found", taskId);
                return Enumerable.Empty<CommentDto>();
            }

            List<CommentDto> commentDtos = new List<CommentDto>();
            foreach (var comment in comments)
            {
                var commentDto = new CommentDto
                {
                    Id = comment.Id,
                    CommentText = comment.CommentText,
                    Author = comment.Author,
                    RelatedTaskId = comment.RelatedTaskId
                };
                commentDtos.Add(commentDto);
            }

            _logger.LogInformation("Found {Count} comments", commentDtos.Count);
            return commentDtos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comments");
            return Enumerable.Empty<CommentDto>();
        }
    }

    public async Task<CommentDto?> Create(CreateCommentDto model)
    {
        try
        {
            var relatedTask = await _commentRepository.GetRelatedTaskAsync(model.RelatedTaskId);
            if (relatedTask == null)
            {
                _logger.LogWarning("Related task with id: {Id} not found", model.RelatedTaskId);
                return null;
            }

            var comment = new CommentModel
            {
                CommentText = model.CommentText,
                Author = model.Author,
                RelatedTaskId = model.RelatedTaskId
            };
            var validationResults = ValidationService.Validate(comment);
            if (validationResults.Any())
            {
                _logger.LogWarning("Validation failed: {Errors}",
                    string.Join(" | ", validationResults.Select(r => r.ErrorMessage)));
                return null;
            }
            await _commentRepository.AddAsync(comment);
            _logger.LogInformation("Created comment with id: {Id}", comment.Id);
            var commentDto = new CommentDto
            {
                Id = comment.Id,
                CommentText = comment.CommentText,
                Author = comment.Author,
                RelatedTaskId = comment.RelatedTaskId
            };
            
            return commentDto;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating comment");
            return null;
        }
    }

    public async Task<bool> Update(UpdateCommentDto model)
    {
        try
        {
            var relatedTask = await _commentRepository.GetRelatedTaskAsync(model.RelatedTaskId);
            if (relatedTask == null)
            {
                _logger.LogWarning("Task with id: {Id} not found", model.RelatedTaskId);
                return false;
            }

            var comment = await _commentRepository.GetByIdAsync(model.Id);
            if (comment == null)
            {
                _logger.LogWarning("Comment with id: {Id} not found", model.Id);
                return false;
            }


            comment.CommentText = model.CommentText;
            comment.Author = model.Author;
            comment.RelatedTaskId = model.RelatedTaskId;
            var validationResults = ValidationService.Validate(comment);
            if (validationResults.Any())
            {
                _logger.LogWarning("Validation failed: {Errors}",
                    string.Join(" | ", validationResults.Select(r => r.ErrorMessage)));
                return false;
            }
            await _commentRepository.UpdateAsync(comment);
            _logger.LogInformation("Updated comment with id: {Id}", model.Id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating comment with id: {Id}", model.Id);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                _logger.LogWarning("Comment with id: {Id} not found", id);
                return false;
            }

            await _commentRepository.DeleteAsync(comment);
            _logger.LogInformation("Deleted comment with id: {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting comment with id: {Id}", id);
            return false;
        }
    }
}