using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlash : MonoBehaviour
{
    public GameObject slashGameObject;
    private Slash slash;

    private void Awake() {
        slash = slashGameObject.GetComponent<Slash>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 direction = other.transform.position - transform.position;
            if (Vector3.Dot(transform.right, direction) < 0) {
                Debug.Log("back");
                slash.activate(true);
            }
            if (Vector3.Dot(transform.right, direction) > 0) {
                Debug.Log("front");
                slash.activate(false);
            }
        }
    }
}
