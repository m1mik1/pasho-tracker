using PaSho_Tracker.Interface;

namespace PaSho_Tracker.Domain.Model;

public abstract class BaseEntity : IEntity
{
    protected BaseEntity()
    {
    }


    protected BaseEntity(int id)
    {
        Id = id;
    }

    public int Id { get; init; }
    public DateTime CreatedDate { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    public abstract string GetEntityInfo();

    public virtual void GetCreationDate()
    {
        Console.WriteLine($"Created Date: {CreatedDate}");
    }
}