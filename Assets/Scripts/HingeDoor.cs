using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeDoor : MonoBehaviour
{
    private float timer;
    public GameObject leftDoor;
    public GameObject rightDoor;

    private void Awake() {
        leftDoor = GetComponentInChildren<GameObject>();
        rightDoor = GetComponentInChildren<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                closeDoor();
            }
        }
    }

    public void openDoor() {
        leftDoor.transform.RotateAround(new Vector3(leftDoor.transform.position.x, 0, leftDoor.transform.localScale.z), Vector3.up, 120);
        rightDoor.transform.RotateAround(new Vector3(rightDoor.transform.position.x, 0, rightDoor.transform.localScale.z), Vector3.down, 120);
    }

    public void closeDoor() {
        leftDoor.transform.RotateAround(new Vector3(leftDoor.transform.position.x, 0, 2* leftDoor.transform.position.z), Vector3.down, 120);
        rightDoor.transform.RotateAround(new Vector3(rightDoor.transform.position.x, 0, 2* rightDoor.transform.position.z), Vector3.up, 120);
    }

    private void OnTriggerEnter(Collider other) {
        timer = 1f;
        if (other.tag == "Player") {
            openDoor();
        }
    }
}
