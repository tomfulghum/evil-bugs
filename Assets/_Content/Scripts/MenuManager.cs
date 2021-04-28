using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Metamothosis.Serializable;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject settingsMenu;

    [Header("Settings")]
    [SerializeField] Slider mouseSensitivitySlider;
    [SerializeField] TextMeshProUGUI mouseSensitivityValue;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] TextMeshProUGUI musicVolumeValue;
    [SerializeField] Slider sfxVolumeSlider;
    [SerializeField] TextMeshProUGUI sfxVolumeValue;

    void OnEnable()
    {
        SettingsManager.OnSettingsChanged += OnSettingsChanged;
        SettingsManager.OnSettingsInitialized += OnSettingsInitialized;
    }

    void OnDisable()
    {
        SettingsManager.OnSettingsChanged -= OnSettingsChanged;
        SettingsManager.OnSettingsInitialized -= OnSettingsInitialized;
    }

    public void ShowMainMenu(bool show)
    {
        mainMenu.SetActive(show);
    }

    public void ShowPauseMenu(bool show)
    {
        pauseMenu.SetActive(show);
    }

    public void ShowWinMenu(bool show)
    {
        winMenu.SetActive(show);
    }

    public void ShowSettingsMenu(bool show)
    {
        settingsMenu.SetActive(show);
    }

    void OnSettingsChanged(Settings settings)
    {
        mouseSensitivityValue.text = settings.mouseSensitivity.ToString("N2");
        musicVolumeValue.text = settings.musicVolume.ToString("N2");
        sfxVolumeValue.text = settings.sfxVolume.ToString("N2");
    }

    void OnSettingsInitialized(Settings settings)
    {
        mouseSensitivitySlider.value = settings.mouseSensitivity;
        musicVolumeSlider.value = settings.musicVolume;
        sfxVolumeSlider.value = settings.sfxVolume;

        OnSettingsChanged(settings);
    }
}
