using System.IO;
using UnityEngine;

namespace Metamothosis.Utility
{
    public static class JsonLoader
    {
        public static T Load<T>(string _filePath)
        {
            if (!File.Exists(_filePath))
                return default;

            Debug.Log("Loading " + _filePath);
            string text = File.ReadAllText(_filePath);
            T obj = JsonUtility.FromJson<T>(text);

            return obj;
        }
    }
}
