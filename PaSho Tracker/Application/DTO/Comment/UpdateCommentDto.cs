namespace PaSho_Tracker.DTO;

public class UpdateCommentDto
{
    public int Id { get; set; }
    public string CommentText { get; set; } = null!;
    public string Author { get; set; } = null!;
    public int RelatedTaskId { get; set; }
}