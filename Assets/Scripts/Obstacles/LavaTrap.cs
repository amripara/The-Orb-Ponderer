using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrap : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Sounds.PlaySound(Sounds.Sound.Player_Death_Burn);
            player.GetComponent<PlayerController>().KillPlayer();
        }
    }
}
