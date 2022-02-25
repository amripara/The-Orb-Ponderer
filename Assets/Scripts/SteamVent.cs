using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVent : MonoBehaviour
{
    private bool activated = false;
    public float speed_up = 4f;
    public float speed_forward = 1f;

    public float angle = 90f;
    public ParticleSystem[] particles;

    private void Awake() {
        particles = GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem particle in particles)
            particle.Stop();
    }

    public void activate() {
        Debug.Log("vent activated");
        activated = true;
        foreach (ParticleSystem particle in particles)
            particle.Play();
    }

    public void deactivate() {
        Debug.Log("vent deactivated");
        activated = false;
        foreach (ParticleSystem particle in particles)
            particle.Stop();
    }


    private void OnTriggerStay(Collider other) {
        Debug.Log("Entered the vent");
        if (activated && other.attachedRigidbody) {
            other.attachedRigidbody.AddForce(Vector3.up * speed_up);
            other.attachedRigidbody.AddForce(Vector3.forward * speed_forward);
        }
    }
}
