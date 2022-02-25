using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    private static PlayerSettings instance = null;
    private static float masterVolumeValue = 0.75f;

     void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public float GetMasterVolume()
    {
        return masterVolumeValue;
    }

    public void SetMasterVolume(float value)
    {
        masterVolumeValue = value;
    }
}
