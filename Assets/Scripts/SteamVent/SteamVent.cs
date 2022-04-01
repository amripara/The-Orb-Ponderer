using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVent : MonoBehaviour
{
    private bool activated = false;
    public float speed_up = 50f;
    public float speed_forward = 10f;

    public ParticleSystem[] particles;
    public VentCover[] ventCovers;
    private float timer;
    private AudioSource audioSource;
    private Rigidbody player;


    private void Awake() {
        particles = GetComponentsInChildren<ParticleSystem>();
        ventCovers = GetComponentsInChildren<VentCover>();
        audioSource = GetComponent<AudioSource>();

        foreach (ParticleSystem particle in particles)
            particle.Stop();

    }

    private void Update() {
        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0.2 && player != null)
            {
                player.AddForce(player.transform.forward * speed_forward);
            }
            if (timer <= 0f) {
                deactivate();
            }
        }
    }

    public void activate() {
        activated = true;
        foreach (VentCover cover in ventCovers) 
            cover.open();
        foreach (ParticleSystem particle in particles)
            particle.Play();
        audioSource.Play();
    }

    public void deactivate() {
        activated = false;
        foreach (VentCover cover in ventCovers) 
            cover.close();
        foreach (ParticleSystem particle in particles)
            particle.Stop();
        audioSource.Stop();
    }


    private void OnTriggerStay(Collider other) {
        timer = 1f;
        if (activated && other.attachedRigidbody) {
            player = other.attachedRigidbody;
            other.attachedRigidbody.AddForce(Vector3.up * speed_up);
            other.attachedRigidbody.AddForce(other.transform.forward * speed_forward);
        }
    }
}
