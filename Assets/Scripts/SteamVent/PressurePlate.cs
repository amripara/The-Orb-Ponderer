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
        initial = playerCam.transform.localRotation;
    }

    // Changes camera view to inform the player it is an elevator, not a trap
    private void FixedUpdate()
    {   

        //Debug.Log(-gameObject.transform.forward);
        if (enteredFromFront) {
            if (timer > 0) {
                timer -= Time.deltaTime;
                if (timer >= 1.0f) {
                    Debug.Log("look up" + timer);
                    playerCam.transform.Rotate(Vector3.left, 30.0f * Time.deltaTime);
                } else if (timer >= 0f) {
                    Debug.Log("look down" + timer);
                    playerCam.transform.Rotate(Vector3.right, 60.0f * Time.deltaTime);
                }
                if (timer <= 0f) {
                    Debug.Log("stop");
                    enteredFromFront = false;
                    timer = 2.5f;
                    playerCam.transform.localRotation = initial;
                }
            }
        }   
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Player") {
            //Debug.Log(other.gameObject.transform.forward);
            //Debug.Log(Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward));
            if (Mathf.Ceil(Vector3.Dot(other.gameObject.transform.forward, gameObject.transform.forward)) == 1) {
                timer = 2.5f;
                enteredFromFront = true;
                Debug.Log("entered from the front");
            } 
            vent.activate();
        }
    }
}
