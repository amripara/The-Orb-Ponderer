using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sounds
{
    public enum Sound {
        PlayerRun_Wood,
        PlayerSlide_Wood,
        PlayerJump1,
        PlayerJump2
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerRun_Wood] = -5f;
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
        case Sound.PlayerRun_Wood:
            if (soundTimerDictionary.ContainsKey(sound)) {
                float lastTimePlayed = soundTimerDictionary[sound];
                float playerMoveTimerMax = 5f;
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
        soundTimerDictionary[Sound.PlayerRun_Wood] = -5f;
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
