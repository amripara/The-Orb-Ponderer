using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public SoundAudioClip[] soundAudioClipArray;
    
    private void Awake()
    {
        if (instance != null) {
            Destroy(this);
        } else {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    public static SoundManager GetSoundManager()
    {
        return instance;
    }
    
    [System.Serializable]
    public class SoundAudioClip
    {
        public Sounds.Sound sound;
        public AudioClip audioClip;
    }
}