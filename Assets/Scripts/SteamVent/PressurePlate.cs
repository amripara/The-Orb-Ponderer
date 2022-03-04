using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject ventGameObject;
    private SteamVent vent;


    private void Awake() {
        vent = ventGameObject.GetComponent<SteamVent>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            vent.activate();
            //other.GetComponent<ParticleSystem>().Play();
        }
    }
}
