using System.IO;
using UnityEngine;

public class JsonSaver
{
    public static void Save(object _object, string _fileName)
    {
        string json = JsonUtility.ToJson(_object, true);
        Save(json, _fileName);
    }

    public static void Save(string _json, string _filePath)
    {
        string path = Path.GetDirectoryName(_filePath);
        if (Directory.Exists(Path.GetDirectoryName(path)) == false)
            Directory.CreateDirectory(Path.GetDirectoryName(path));

        Debug.Log("Saving " + _filePath);
        StreamWriter writer = new StreamWriter(_filePath, false);
        writer.Write(_json);
        writer.Close();
    }
}
