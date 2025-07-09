using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class DataLocalProvider : IDataProvider
{
    private const string FileName = "PlayerSave";
    private const string SaveFileExtention = ".json";

    private IPersistentData persistentData;

    public DataLocalProvider(IPersistentData _persistentData) => persistentData = _persistentData;

    private string SavePath => Application.persistentDataPath;
    private string FullPath => Path.Combine(SavePath, $"{FileName}{SaveFileExtention}");

    public bool TryLoad()
    {
        if (IsDataAlreadyExist() == false)
            return false;

        persistentData.PlayerData = JsonConvert.DeserializeObject<PlayerData>(File.ReadAllText(FullPath));
        return true;
    }

    

    public void Save()
    {
        File.WriteAllText(FullPath, JsonConvert.SerializeObject(persistentData.PlayerData, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));
    }

    public void ResetSave()
    {
        if (IsDataAlreadyExist())
        {
            File.Delete(FullPath);
            Debug.Log("Save delete");
        }
        else
        {
            Debug.LogWarning("no save file");
        }
    }

    private bool IsDataAlreadyExist() => File.Exists(FullPath);

}
