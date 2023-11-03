
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveLoadResetController
{
    public static void savePlayerData(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "SaveData.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameDataController data = new GameDataController(player);

        formatter.Serialize(stream, data);
        Debug.Log("Saving...");
        stream.Close();
    }
    public static void resetPlayerData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "SaveData.txt";
        File.Delete(path);
    }

    public static GameDataController loadPlayerData()
    {
        string path = Application.persistentDataPath + "SaveData.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameDataController data = formatter.Deserialize(stream) as GameDataController;
            stream.Close();
            return data;
        } else
        {
            Debug.LogError("Save file not found error" + path);
            return null;
        }
        
    }
}
