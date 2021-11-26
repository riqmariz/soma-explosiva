using System.IO;
using UnityEngine;

public class JsonSaveLoader : MonoBehaviour, ISaveLoader
{
    [SerializeField] private bool useEncryption;

    private Encryptor _encryptor;

    public T Load<T>(string path) where T : class
    {
        if (_encryptor == null)
            _encryptor = new Encryptor();

        FileStream fs = new FileStream(path, FileMode.Open);

        StreamReader reader = new StreamReader(fs);

        var json = reader.ReadToEnd();

        if (useEncryption)
            json = _encryptor.Decrypt(json);

        //Debug.Log(json);

        var data = JsonUtility.FromJson<T>(json);

        reader.Close();
        reader.Dispose();

        fs.Close();
        fs.Dispose();

        return data;
    }

    public void Save<T>(T data, string path) where T : class
    {
        if (_encryptor == null)
            _encryptor = new Encryptor();

        FileStream fs = new FileStream(path, FileMode.Create);

        StreamWriter writer = new StreamWriter(fs);

        string jsonData = JsonUtility.ToJson(data);

        string fileContent = jsonData;
        if (useEncryption)
            fileContent = _encryptor.Encrypt(fileContent);

        //Debug.Log(fileContent);

        writer.Write(fileContent);
        writer.Close();
        writer.Dispose();

        fs.Close();
        fs.Dispose();
    }
}
