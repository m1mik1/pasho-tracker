using System.Text.Json;

namespace PaSho_Tracker.Infrastructure.Storage;

using System.Text;

public static class JsonFileHelper
{
    public static void WriteToFile(string filePath, string content)
    {
        File.WriteAllText(filePath, content, Encoding.UTF8);
    }

    public static string ReadFromFile(string filePath)
    {
        return File.Exists(filePath) ? File.ReadAllText(filePath, Encoding.UTF8) : string.Empty;
    }
    
    public static void SaveToFile<T>(string path, List<T> data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path, json);
    }

    public static List<T> LoadFromFile<T>(string path)
    {
        if (!File.Exists(path))
            return new List<T>();

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
    }
}
