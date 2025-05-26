using PaSho_Tracker.Enums;
using TaskStatus = PaSho_Tracker.Enums.TaskStatus;

namespace PaSho_Tracker.DTO;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime Deadline { get; set; }
    public string AssignedUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}