using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    private static PlayerSettings instance = null;
    private static float sfxVolumeValue = 0.75f, musicVolumeValue = 0.5f;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        //Debug.Log(sfxVolumeValue);
    }

    public float GetSFXVolume()
    {
        return sfxVolumeValue;
    }

    public float GetMusicVolume()
    {
        return musicVolumeValue;
    }

    public void SetSFXVolume(float value)
    {
        sfxVolumeValue = value;
    }

    public void SetMusicVolume(float value)
    {
        musicVolumeValue = value;
    }
}
