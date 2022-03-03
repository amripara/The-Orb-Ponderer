

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
        animator.SetBool("Open", true);
    }

    public void CloseDoor() {
        animator.SetBool("Open", false);
    }

    private void OnTriggerEnter(Collider other) {
        OpenDoor();
    }

    private void OnTriggerExit(Collider other) {
        CloseDoor();
    }
}
