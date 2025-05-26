namespace PaSho_Tracker.Contracts.Storage;

public interface ISerializer<T>
{
    string Serialize(List<T> data);
    List<T> Deserialize(string serializedData);
}
