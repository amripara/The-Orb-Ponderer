using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider sfxVolumeSlider;
    
    private PlayerSettings playerSettingsScript = null;
    
    void Start()
    {
        playerSettingsScript = GameObject.Find("PlayerSettings").GetComponent<PlayerSettings>();
        sfxVolumeSlider.value = playerSettingsScript.GetSFXVolume();
    }

    public void SetMasterVolume ()
    {
        playerSettingsScript.SetSFXVolume(sfxVolumeSlider.value);
        masterMixer.SetFloat("MasterVol", Mathf.Log10(sfxVolumeSlider.value) * 20);
    }
}
