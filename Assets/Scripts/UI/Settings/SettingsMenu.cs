using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    private Resolution[] _resolutions;

    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    
    
    void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;
        
        _resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        
        for (int i = 0; i < _resolutions.Length; i++)
        {
            Resolution current = _resolutions[i];
            options.Add($"{current.width} x {current.height} | {current.refreshRate}Hz");

            if (current.width == Screen.currentResolution.width &&
                current.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
                
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    #region Audio

    public void SetFxVolume(float volume)
    {
        SetVolume(volume, "EffectsVolume");
    }

    public void SetBackgroundVolume(float volume)
    {
        SetVolume(volume, "BackgroundVolume");
    }
    
    public void SetMasterVolume(float volume)
    {
        SetVolume(volume, "MasterVolume");
    }
    
    private void SetVolume(float volume, string mixerParam)
    {
        audioMixer.SetFloat(mixerParam, Mathf.Lerp(-50, 0, volume));
    }

    #endregion


    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

}
