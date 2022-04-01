using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> music = new List<AudioClip>();

    private AudioSource musicSource;
    private static MusicManager instance = null;
    
    void Awake()
    {
        musicSource = this.GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SetMusic(int trackNum)
    {
        musicSource.Stop();
        switch(trackNum) {
            case 0: // menu music
                musicSource.clip = music[0];
                break;
            case 1: // in-game music
                musicSource.clip = music[1];
                break;
        }
        musicSource.Play();
    }

    public void PlayMusic(bool value)
    {
        if (value) {
            musicSource.Play();
        } else {
            musicSource.Stop();
        }
    }
}
