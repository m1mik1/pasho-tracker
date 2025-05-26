using PaSho_Tracker.Contracts.Storage;
using PaSho_Tracker.Domain.Model;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Application;

public class GenericRepository<T> where T : BaseEntity
{
    private readonly IDataStorage<T> _storage;

    public GenericRepository(IDataStorage<T> storage)
    {
        _storage = storage;
    }

    public List<T> GetAll() => _storage.GetAll();

    public T? GetById(int id) => _storage.GetById(id);

    public void Add(T item)
    {
        _storage.Add(item);
        _storage.Save();
    }

    public void Update(T item)
    {
        _storage.Update(item);
        _storage.Save();
    }

    public void Delete(int id)
    {
        _storage.Delete(id);
        _storage.Save();
    }
}
