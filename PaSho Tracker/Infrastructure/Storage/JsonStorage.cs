using System.Text.Json;
using System.Text.Json.Serialization;
using PaSho_Tracker.Contracts.Storage;
using PaSho_Tracker.Domain.Model;
using PaSho_Tracker.Model;

namespace PaSho_Tracker.Infrastructure.Storage;

public class JsonStorage<T> : IDataStorage<T> where T : BaseEntity
{
    private readonly string _filePath;
    private List<T> _items;

    public JsonStorage()
    {
        var folder = Path.Combine(Directory.GetCurrentDirectory(), "App_Data");
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        _filePath = Path.Combine(folder, $"{typeof(T).Name}.json");
        _items = LoadFromFile();
    }

    public void Add(T entity)
    {
        _items.Add(entity);
        Save();
    }

    public void Update(T entity)
    {
        var index = _items.FindIndex(x => x.Id == entity.Id);
        if (index != -1)
        {
            _items[index] = entity;
            Save();
        }
    }

    public void Delete(int id)
    {
        _items.RemoveAll(x => x.Id == id);
        Save();
    }

    public T? GetById(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public List<T> GetAll()
    {
        return _items;
    }

    public void Save()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(_items, options);
        File.WriteAllText(_filePath, json);
    }

    private List<T> LoadFromFile()
    {
        if (!File.Exists(_filePath))
            return new List<T>();

        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
}