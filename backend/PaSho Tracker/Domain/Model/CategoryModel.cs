using PaSho_Tracker.Model;

namespace PaSho_Tracker.Domain.Model;

using System.ComponentModel.DataAnnotations;

public class CategoryModel : BaseEntity
{
    public CategoryModel(int id, string categoryName) : base(id)
    {
        CategoryName = categoryName;
    }

    public CategoryModel() {}

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be 2 to 100 characters long.")]
    public string CategoryName { get; set; }

    public override string GetEntityInfo()
    {
        return $"CategoryName = {CategoryName}";
    }
}
