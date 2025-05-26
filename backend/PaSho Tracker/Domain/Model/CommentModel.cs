using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PaSho_Tracker.Domain.Model;

namespace PaSho_Tracker.Model;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class CommentModel : BaseEntity
{
    public CommentModel() {}

    public CommentModel(int id, string commentText, string author, int relatedTaskId) : base(id)
    {
        CommentText = commentText;
        Author = author;
        RelatedTaskId = relatedTaskId;
    }

    [Required(ErrorMessage = "Comment text is required.")]
    [StringLength(500, ErrorMessage = "Comment cannot be longer than 500 characters.")]
    public string CommentText { get; set; }

    [Required(ErrorMessage = "Author is required.")]
    [StringLength(100, ErrorMessage = "Author name cannot be longer than 100 characters.")]
    public string Author { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Related task ID must be greater than 0.")]
    public int RelatedTaskId { get; set; }

    [JsonIgnore]
    [BindNever] 
    public TaskModel? RelatedTask { get; set; }

    public override string GetEntityInfo()
    {
        return $"Comment Text = {CommentText}, Author = {Author}";
    }
}
