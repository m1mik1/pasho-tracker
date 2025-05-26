namespace PaSho_Tracker.DTO;

public class CreateCommentDto
{
    public string CommentText { get; set; } = null!;
    public string Author { get; set; }  = null!;
    public int RelatedTaskId { get; set; }
}