using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioMixerGroup masterVolMixer;
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