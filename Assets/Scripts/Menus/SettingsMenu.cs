using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider masterVolumeSlider;
    
    private PlayerSettings playerSettingsScript = null;
    
    void Start()
    {
        playerSettingsScript = GameObject.Find("PlayerSettings").GetComponent<PlayerSettings>();
        masterVolumeSlider.value = playerSettingsScript.GetMasterVolume();
    }

    public void SetMasterVolume ()
    {
        playerSettingsScript.SetMasterVolume(masterVolumeSlider.value);
        masterMixer.SetFloat("MasterVol", Mathf.Log10(masterVolumeSlider.value) * 20);
    }
}
