using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingedDoor : MonoBehaviour
{
    private Animator animator;

    private float timer = 0f;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                CloseDoor();
            }
        }
    }

    public void OpenDoor() {
        timer = 1f;
        animator.SetBool("Open", true);
    }

    public void CloseDoor() {
        animator.SetBool("Open", false);
    }

    // private void OnTriggerEnter(Collider other) {
    //     timer = 1f;
    //     if (other.tag == "Player") {
    //         OpenDoor();
    //     }
    // }

}
