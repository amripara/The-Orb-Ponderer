using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject ventGameObject;
    private SteamVent vent;

    public GameObject playerCam;
    private Quaternion initial;
    private bool enteredFromFront;
    public float timer;

    private void Awake() {
        vent = ventGameObject.GetComponent<SteamVent>();
        initial = playerCam.transform.rotation;
    }

    // Changes camera view to inform the player it is an elevator, not a trap
    private void Update()
    {   
        Debug.Log(timer);
        if (enteredFromFront) {
            if (timer > 0) {
                timer -= Time.deltaTime;
                if (timer >= 1.0f) {
                    Debug.Log("look up" + timer);
                    playerCam.transform.Rotate(Vector3.left, 30.0f * Time.deltaTime);
                } else if (timer >= 0f && playerCam.transform.position.x > initial.x) {
                    Debug.Log("look down" + timer);
                    playerCam.transform.Rotate(Vector3.right, 30.0f * Time.deltaTime);
                }
                if (timer <= 0f) {
                    Debug.Log("stop");
                    enteredFromFront = false;
                    timer = 2.5f;
                    playerCam.transform.rotation = initial;
                }
            }
        }   
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Player") {
            Debug.Log(other.gameObject.transform.position.z + " " + transform.position.z);
            if (playerCam.transform.position.z < transform.position.z + 1) {
                timer = 2.5f;
                enteredFromFront = true;
                Debug.Log("entered from the front");
            } 
            vent.activate();
        }
    }
}
