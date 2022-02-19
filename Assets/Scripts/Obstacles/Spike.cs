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
            playerScript.KillPlayer();
        }
    }
}
