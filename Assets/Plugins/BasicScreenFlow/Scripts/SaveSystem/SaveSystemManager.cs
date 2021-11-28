
using System.IO;
using UnityEngine;

public class SaveSystemManager : Singleton<SaveSystemManager>
{
    private PlayerData _data;
    private ISaveLoader _saveloader;

    private string _filepath;

    bool firstAccess = true;


    void FirstAccess()
    {
        this._filepath = Application.persistentDataPath + "/player.data";
        Debug.Log(_filepath);

        if (_saveloader == null)
            _saveloader = gameObject.GetComponent<ISaveLoader>();

        Load();
    }

    public PlayerData GetPlayerData()
    { 
        if (firstAccess) 
        {
            FirstAccess();
            firstAccess = false;
        }
        return _data;
    }

    private void Load()
    {
        if (File.Exists(_filepath))
        {
            _data = _saveloader.Load<PlayerData>(_filepath);
        }
        else
        {
            _data = new PlayerData();
        }
    }

    public void Save()
    {
        _saveloader.Save(_data, _filepath);
    }
}
