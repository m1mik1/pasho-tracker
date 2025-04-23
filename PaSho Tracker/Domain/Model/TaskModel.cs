using System.Text.Json.Serialization;
using PaSho_Tracker.Enums;
using PaSho_Tracker.Model;
using TaskStatus = PaSho_Tracker.Enums.TaskStatus;

namespace PaSho_Tracker.Domain.Model;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PaSho_Tracker.Enums;

public class TaskModel : BaseEntity
{
    public TaskModel(int id, string title, string description, TaskPriority priority, TaskStatus status,
        DateTime deadline, string assignedUserId)
        : base(id)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Status = status;
        Deadline = deadline;
        AssignedUserId = assignedUserId;
        Comments = new List<CommentModel>();
    }

    public TaskModel()
    {
        Comments = new List<CommentModel>();
    }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
    public string Title { get; set; }

    [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
    public string Description { get; set; }

    [Required]
    [EnumDataType(typeof(TaskPriority))]
    public TaskPriority Priority { get; set; }

    [Required]
    [EnumDataType(typeof(TaskStatus))]
    public TaskStatus Status { get; set; }

    [Required(ErrorMessage = "Deadline is required.")]
    public DateTime Deadline { get; set; }

    [Required(ErrorMessage = "Assigned user ID is required.")]
    public string AssignedUserId { get; set; }

    [JsonIgnore]
    public ICollection<CommentModel> Comments { get; set; }

    public override string GetEntityInfo()
    {
        return $"Title = {Title}, Description = {Description}";
    }
}
