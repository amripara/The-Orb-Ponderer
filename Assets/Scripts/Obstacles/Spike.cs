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
            int rand = Random.Range(0,2);
            if (rand == 0) {
                Sounds.PlaySound(Sounds.Sound.SweepingSpikes_Hit1);
            } else {
                Sounds.PlaySound(Sounds.Sound.SweepingSpikes_Hit2);
            }
            playerScript.KillPlayer();
        }
    }
}
