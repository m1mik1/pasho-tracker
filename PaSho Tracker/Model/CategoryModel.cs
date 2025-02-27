namespace PaSho_Tracker.Model;

public class CategoryModel : BaseEntity
{
    public CategoryModel(int id, string categoryName) : base(id)
    {
        CategoryName = categoryName;
    }

    public string CategoryName { get; set; }

    public override string GetEntityInfo()
    {
        return $"CategoryName = {CategoryName}";
    }
}