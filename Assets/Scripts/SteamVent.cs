using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamVent : MonoBehaviour
{
    private bool activated;
    public float speed_up = 40f;
    public float speed_forward = 10f;

    public GameObject cover0;   
    public GameObject cover1;   
    public GameObject cover2;   
    public GameObject cover3;   
    public GameObject cover4;   
    public GameObject cover5;   
    public GameObject cover6;   
    public GameObject cover7;   
    public GameObject cover8;   
    public GameObject cover9;   
    private VentCover ventCover0;
    private VentCover ventCover1;
    private VentCover ventCover2;
    private VentCover ventCover3;
    private VentCover ventCover4;
    private VentCover ventCover5;
    private VentCover ventCover6;
    private VentCover ventCover7;
    private VentCover ventCover8;
    private VentCover ventCover9;



    private void Awake() {
        ventCover0 = cover0.GetComponent<VentCover>();
        ventCover1 = cover1.GetComponent<VentCover>();
        ventCover2 = cover2.GetComponent<VentCover>();
        ventCover3 = cover3.GetComponent<VentCover>();
        ventCover4 = cover4.GetComponent<VentCover>();
        ventCover5 = cover5.GetComponent<VentCover>();
        ventCover6 = cover6.GetComponent<VentCover>();
        ventCover7 = cover7.GetComponent<VentCover>();
        ventCover8 = cover8.GetComponent<VentCover>();
        ventCover9 = cover9.GetComponent<VentCover>();
    }

    public Transform parent;
    public float angle = 90f;

    public void activate() {
        //start particle animation
        transform.RotateAround(parent.position, Vector3.up, angle * Time.deltaTime);
        activated = true;
    }

    public void deactivate() {
        //stop particle animation
        activated = false;
    }


    private void OnTriggerStay(Collider other) {
        Debug.Log("Entered the vent");
        if (activated && other.attachedRigidbody) {
            other.attachedRigidbody.AddForce(Vector3.up * speed_up);
            other.attachedRigidbody.AddForce(Vector3.forward * speed_forward);
        }
    }
}
