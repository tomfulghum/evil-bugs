using UnityEngine;
using UnityEngine.Audio;
using Metamothosis.Serializable;

public class VolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    void OnEnable()
    {
        SettingsManager.OnSettingsChanged += OnSettingsChanged;
    }

    void OnDisable()
    {
        SettingsManager.OnSettingsChanged -= OnSettingsChanged;
    }

    public void OnSettingsChanged(Settings settings)
    {
        mixer.SetFloat("MusicVolume", LogarithmizeVolume(settings.musicVolume));
        mixer.SetFloat("SfxVolume", LogarithmizeVolume(settings.sfxVolume));
    }

    float LogarithmizeVolume(float volume)
    {
        return Mathf.Log10(volume) * 20f;
    }
}
