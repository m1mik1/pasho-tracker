using System.Text.Json.Serialization;
using PaSho_Tracker.Enums;
using TaskStatus = PaSho_Tracker.Enums.TaskStatus;

namespace PaSho_Tracker.Model;

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

    public string Title { get; set; }
    public string Description { get; set; }

    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }

    public DateTime Deadline { get; set; }
    public string AssignedUserId { get; set; }
    [JsonIgnore] public ICollection<CommentModel> Comments { get; set; }

    public override string GetEntityInfo()
    {
        return $"Title = {Title}, Description = {Description}";
    }
}