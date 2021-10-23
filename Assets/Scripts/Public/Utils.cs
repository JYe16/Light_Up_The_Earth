using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    public static string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    public static string ReadDataFromFile(string fileName)
    {
        string path = GetFilePath(fileName);
        Debug.Log(path);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        return "";
    }

    public static void WriteJSON(string fileName, string json)
    {
        fileName = GetFilePath(fileName);
        File.WriteAllText(fileName, json);
    }

	//clear all stored playerprefs
    public static void clearCache()
    {
        PlayerPrefs.DeleteKey("level");
        PlayerPrefs.DeleteKey("baseScore");
        PlayerPrefs.DeleteKey("total");
        PlayerPrefs.DeleteKey("BOMB");
        PlayerPrefs.DeleteKey("POWER_WATER");
        PlayerPrefs.DeleteKey("TIME_INCREASE");
        PlayerPrefs.DeleteKey("SCORE_INCREASE");
    }
}