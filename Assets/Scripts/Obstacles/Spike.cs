using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public PlayerController playerScript;

    private bool killingPlayer = false;
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && !killingPlayer) {
            killingPlayer = true;
            Sounds.StopPlayingRunningSound();
            int rand = Random.Range(0,2);
            if (rand == 0) {
                Sounds.PlaySound(Sounds.Sound.Sweeping_Spikes_Hit1);
            } else {
                Sounds.PlaySound(Sounds.Sound.Sweeping_Spikes_Hit2);
            }
            playerScript.KillPlayer();
        }
    }
}
