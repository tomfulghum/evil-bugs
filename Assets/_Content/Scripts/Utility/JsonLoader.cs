using System.IO;
using UnityEngine;

public class JsonLoader
{
    public static T Load<T>(string _filePath)
    {
        if (!File.Exists(_filePath))
            return default(T);

        Debug.Log("Loading " + _filePath);
        string text = File.ReadAllText(_filePath);
        T obj = (T)JsonUtility.FromJson(text, typeof(T));

        return obj;
    }
}
