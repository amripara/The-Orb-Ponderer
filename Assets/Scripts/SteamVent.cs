using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVent : MonoBehaviour
{
    private bool activated = false;
    public float speed_up = 40f;
    public float speed_forward = 10f;

    public float angle = 90f;

    public ParticleSystem particleSystem;

    private void Awake() {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void activate() {
        Debug.Log("vent activated");
        activated = true;
        particleSystem.Play();
    }

    public void deactivate() {
        Debug.Log("vent deactivated");
        activated = false;
        particleSystem.Stop();
    }


    private void OnTriggerStay(Collider other) {
        Debug.Log("Entered the vent");
        if (activated && other.attachedRigidbody) {
            other.attachedRigidbody.AddForce(Vector3.up * speed_up);
            other.attachedRigidbody.AddForce(Vector3.forward * speed_forward);
        }
    }
}
