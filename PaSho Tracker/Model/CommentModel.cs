using System.Text.Json.Serialization;

namespace PaSho_Tracker.Model;

public class CommentModel : BaseEntity
{
    // Пустой конструктор для EF
    public CommentModel()
    {
    }

    // Конструктор только со scalar-полями
    public CommentModel(int id, string commentText, string author, int relatedTaskId) : base(id)
    {
        CommentText = commentText;
        Author = author;
        RelatedTaskId = relatedTaskId;
    }

    public string CommentText { get; set; }
    public string Author { get; set; }
    public int RelatedTaskId { get; set; }

    [JsonIgnore] public TaskModel RelatedTask { get; set; }

    public override string GetEntityInfo()
    {
        return $"Comment Text = {CommentText}, Author = {Author}";
    }
}