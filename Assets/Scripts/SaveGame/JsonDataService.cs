using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonDataService : IDataService
{
    public T LoadData<T>(string relativePath, bool Encrypted)
    {
        string path = Application.persistentDataPath + relativePath;
        if (!File.Exists(path))
        {
            Debug.LogError($"cannot load file at {path}");
            throw new FileNotFoundException($"{path} does not exist");
        }
        try
        {
            Debug.Log($"Data loaded from {path}");
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }

    public bool SaveData<T>(string relativePath, T data, bool Encrypted)
    {
        string path = Application.persistentDataPath + relativePath;

        try
        {
            if (File.Exists(path))
            {
                Debug.Log("File exist, old one deleted in "+path);
                File.Delete(path);
            }
            else
            {
                Debug.Log("Creat file for the first time");
            }
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }



    }
}
