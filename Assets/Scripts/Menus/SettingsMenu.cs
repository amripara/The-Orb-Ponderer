using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer sfxMixer, musicMixer;
    public Slider sfxVolumeSlider, musicVolumeSlider;
    
    private PlayerSettings playerSettingsScript = null;
    
    void Start()
    {
        playerSettingsScript = GameObject.Find("PlayerSettings").GetComponent<PlayerSettings>();
        sfxVolumeSlider.value = playerSettingsScript.GetSFXVolume();
        musicVolumeSlider.value = playerSettingsScript.GetMusicVolume();
    }

    public void SetSFXVolume ()
    {
        playerSettingsScript.SetSFXVolume(sfxVolumeSlider.value);
        sfxMixer.SetFloat("MasterVol", Mathf.Log10(sfxVolumeSlider.value) * 20);
    }

    public void SetMusicVolume ()
    {
        playerSettingsScript.SetMusicVolume(musicVolumeSlider.value);
        musicMixer.SetFloat("MasterVol", Mathf.Log10(musicVolumeSlider.value) * 20);
    }
}
