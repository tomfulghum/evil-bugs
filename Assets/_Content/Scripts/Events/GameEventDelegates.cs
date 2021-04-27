using Metamothosis.Serializable;

namespace Metamothosis.GameEvents
{
    // Settings
    public delegate void SettingsChangedEvent(Settings settings);

    // Temp
    public delegate void MouseSensitivityChangedEvent(float sensitivity);
}