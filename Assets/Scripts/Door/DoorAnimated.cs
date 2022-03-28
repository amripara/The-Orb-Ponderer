

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimated : MonoBehaviour
{
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor() {
        Sounds.PlaySound(Sounds.Sound.SlidingDoor_Open);
        animator.SetBool("Open", true);
    }

    public void CloseDoor() {
        Sounds.PlaySound(Sounds.Sound.SlidingDoor_Close);
        animator.SetBool("Open", false);
    }

    private void OnTriggerEnter(Collider other) {
        OpenDoor();
    }

    private void OnTriggerExit(Collider other) {
        CloseDoor();
    }
}
