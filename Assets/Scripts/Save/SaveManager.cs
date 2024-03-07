using UnityEngine;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public SaveState SaveState;

    private const string saveFileName = "/Data.Json";


    public Action<SaveState> Loaded;
    public Action<SaveState> Saved;


    private void Awake()
    {
        Instance = this;
        Load();
        //Debug.Log(Application.dataPath + saveFileName);
        Debug.Log(Application.persistentDataPath + saveFileName);
    }

    public void Load()
    {
        try
        {
            string json = File.ReadAllText(Application.persistentDataPath + saveFileName);
            SaveState = JsonUtility.FromJson<SaveState>(json);
            
            Loaded?.Invoke(SaveState);
        }
        catch 
        {
            Debug.Log("Save file not found, let's create a new one!");
            Save();
        }
    }

    public void Save()
    {
        if (SaveState == null)
        {
            SaveState = new SaveState();
        }

        SaveState.LastSaveTime = DateTime.Now;
        
        string json = JsonUtility.ToJson(SaveState);
        File.WriteAllText(Application.persistentDataPath + saveFileName, json);

        Saved?.Invoke(SaveState);
    }
}

