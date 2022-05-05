using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector3 TeleportPosition;
    public GameObject TutoPrompt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Sounds.PlaySound(Sounds.Sound.Menu_Click);
            player.GetComponent<PlayerController>().SetTpSpot(TeleportPosition);
            player.GetComponent<PlayerController>().TpPlayer();
            TutoPrompt.SetActive(true);
        }
    }
}
