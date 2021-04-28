using UnityEngine;
using Metamothosis.Serializable;
using Metamothosis.Utility;

public class SettingsManager : MonoBehaviour
{
    public delegate void SettingsChangedEvent(Settings _);
    public static event SettingsChangedEvent OnSettingsChanged;
    public static event SettingsChangedEvent OnSettingsInitialized;

    Settings settings;

    public float mouseSensitivity
    {
        get {
            return settings.mouseSensitivity;
        }
        set {
            settings.mouseSensitivity = value;
            OnSettingsChanged?.Invoke(settings);
        }
    }

    public float sfxVolume
    {
        get {
            return settings.sfxVolume;
        }
        set {
            settings.sfxVolume = value;
            OnSettingsChanged?.Invoke(settings);
        }
    }

    public float musicVolume
    {
        get {
            return settings.musicVolume;
        }
        set {
            settings.musicVolume = value;
            OnSettingsChanged?.Invoke(settings);
        }
    }

    void Start()
    {
        settings = JsonLoader.Load<Settings>(Application.persistentDataPath + @"\settings.json");
        if (settings == null)
            settings = new Settings();

        OnSettingsInitialized?.Invoke(settings);
    }

    public void SaveSettings()
    {
        JsonSaver.Save(settings, Application.persistentDataPath + @"\settings.json");
    }
}
