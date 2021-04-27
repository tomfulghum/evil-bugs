using UnityEngine;
using TMPro;
using Metamothosis.GameEvents;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] TextMeshProUGUI mouseSensitivityValue;
    [SerializeField] TextMeshProUGUI musicVolumeValue;
    [SerializeField] TextMeshProUGUI sfxVolumeValue;

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

    public void OnMouseSensitivitySliderValueChanged(float value)
    {
        GameEvent<MouseSensitivityChangedEvent>.Trigger?.Invoke(value);
        mouseSensitivityValue.text = value.ToString("N2");
    }

    public void OnMusicVolumeSliderValueChanged(float value)
    {
        musicVolumeValue.text = value.ToString("N2");
    }

    public void OnSfxVolumeSliderValueChanged(float value)
    {
        sfxVolumeValue.text = value.ToString("N2");
    }
}
