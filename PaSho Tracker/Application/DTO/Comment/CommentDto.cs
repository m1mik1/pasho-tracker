namespace PaSho_Tracker.DTO;

public class CommentDto
{
    public int Id { get; set; }
    public string CommentText { get; set; }
    public string Author { get; set; }
    public int RelatedTaskId { get; set; }
    public DateTime CreatedAt { get; set; }
}