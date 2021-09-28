using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    public static string GetFilePath(string fileName)
    {
        return Application.dataPath + "/" + fileName;
    }

    public static string ReadDataFromFile(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            return "";
        }
    }

    public static void WriteJSON(string fileName, string json)
    {
        fileName = GetFilePath(fileName);
        File.WriteAllText(fileName, json);
    }
}