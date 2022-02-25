using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject ventGameObject;
    private SteamVent vent;
    private bool activated = false;

    private void Awake() {
        vent = ventGameObject.GetComponent<SteamVent>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            activated = !activated;
            if (activated) {
                vent.activate();
                Debug.Log("activating steam vent");
            } else {
                vent.deactivate();
                Debug.Log("deactivating steam vent");
            }
        }
    }
}
