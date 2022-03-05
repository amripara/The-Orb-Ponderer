using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sounds
{ 
    public enum Sound 
    {
        // PLAYER //
        PlayerRun_Metal,
        PlayerSlide,
        Player_Jump1,
        Player_Jump2,
        Player_Death_Fall,
        
        // OBSTACLES //

        // Sweeping Spikes
        Sweeping_Spikes_Hit1,
        Sweeping_Spikes_Hit2
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    private static float PlayerRun_Metal_Duration = 3.135f;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerRun_Metal] = -PlayerRun_Metal_Duration;
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound)) {
            if (oneShotGameObject == null) {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch(sound) {
        default:
            return true;
        case Sound.PlayerRun_Metal:
            if (soundTimerDictionary.ContainsKey(sound)) {
                float lastTimePlayed = soundTimerDictionary[sound];
                float playerMoveTimerMax = PlayerRun_Metal_Duration;
                if (lastTimePlayed + playerMoveTimerMax < Time.time) {
                    soundTimerDictionary[sound] = Time.time;
                    return true;
                } else {
                    return false;
                }
            } else {
                return true;
            }
        }
    }

    public static void StopPlayingRunningSound()
    {
        oneShotAudioSource.Stop();
        soundTimerDictionary[Sound.PlayerRun_Metal] = -PlayerRun_Metal_Duration;
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundManager.SoundAudioClip soundAudioClip in SoundManager.GetSoundManager().soundAudioClipArray) {
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
