using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSlash : MonoBehaviour
{
    public Transform Player;
    public float speed = 30;

    public bool activated = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activated)
        {
            transform.LookAt(Player);
            transform.Translate(speed * Vector3.forward * Time.deltaTime);
        }
    }
}
