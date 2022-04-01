using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    private bool activated = false;
    public float speed = 1.0f;
    public float distance = 6.0f;
    public Vector3 originalPosition = new Vector3(3, 0, -1.6f);
    private Vector3 transition = Vector3.right;

    private Transform player;

    // private void FixedUpdate() {
    //     if (activated && (transform.position.x - originalPosition.x < distance)) {
    //         transform.position += transition * speed * Time.deltaTime;
    //     } else {
    //         activated = false;
    //         //transform.position = originalPosition;
    //     }
    // }

    public void activate(bool front) {
        if (!front) {
            setDirection();
        }
        activated = true;
    }

    public void setDirection() {
        originalPosition = new Vector3(-3,0,-1.6f);
        transform.position = originalPosition; 
        transform.Rotate(0.0f, 180.0f, 0.0f);
        transition = Vector3.left;
    }

    // public PlayerController playerScript;

    // private bool killingPlayer = false;

    // void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.tag == "Player" && !killingPlayer) {
    //         killingPlayer = true;
    //         playerScript.KillPlayer();
    //     }
    // }
}
