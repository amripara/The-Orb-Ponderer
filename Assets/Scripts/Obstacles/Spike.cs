using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private PlayerController playerScript;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player") {
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
