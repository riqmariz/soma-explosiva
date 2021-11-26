using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class BinarySaveLoader : MonoBehaviour, ISaveLoader
{
    private BinaryFormatter _formatter;

    public T Load<T>(string path) where T : class
    {
        if (_formatter == null)
            _formatter = new BinaryFormatter();

        FileStream fs = new FileStream(path, FileMode.Open);

        var data = _formatter.Deserialize(fs) as T;

        fs.Close();
        fs.Dispose();

        return data;
    }

    public void Save<T>(T data, string path) where T : class
    {
        if (_formatter == null)
            _formatter = new BinaryFormatter();

        FileStream fs = new FileStream(path, FileMode.Create);

        _formatter.Serialize(fs, data);

        fs.Close();
        fs.Dispose();
    }
}
