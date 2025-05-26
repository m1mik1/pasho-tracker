using PaSho_Tracker.Domain.Model;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Contracts.Storage;

public interface IDataStorage<T> where T : BaseEntity
{
    void Add(T item);
    void Update(T item);
    void Delete(int id);
    T? GetById(int id);
    List<T> GetAll();
    void Save();
}

