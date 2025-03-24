using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PaSho_Tracker.Model;

public class CommentModel : BaseEntity
{
    public CommentModel() {}

    public CommentModel(int id, string commentText, string author, int relatedTaskId) : base(id)
    {
        CommentText = commentText;
        Author = author;
        RelatedTaskId = relatedTaskId;
    }

    public string CommentText { get; set; }
    public string Author { get; set; }
    public int RelatedTaskId { get; set; }

    [JsonIgnore]
    [BindNever] 
    public TaskModel? RelatedTask { get; set; }

    public override string GetEntityInfo()
    {
        return $"Comment Text = {CommentText}, Author = {Author}";
    }
}