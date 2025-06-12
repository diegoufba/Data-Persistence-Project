using System;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance { get; private set; }
    public string currentPlayerName = "";
    public string bestPlayerName = "";
    public int bestScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [Serializable]
    class SaveData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.bestPlayerName = currentPlayerName;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadSave()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.bestPlayerName;
            bestScore = data.bestScore;
        }
    }
}
