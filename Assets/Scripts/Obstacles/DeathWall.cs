using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWall : MonoBehaviour
{
    [SerializeField] private bool isActive;

    private ParticleSystem particles;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().KillPlayer();
        }
    }

    public void ToggleDeathWallActive()
    {
        isActive = !isActive;

        if (particles == null)
        {
            particles = GetComponent<ParticleSystem>();
        }
        if (isActive)
        {
            particles.Play();
        } else
        {
            particles.Stop();
        }
    }
}
