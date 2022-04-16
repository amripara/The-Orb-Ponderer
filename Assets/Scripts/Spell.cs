using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int type = 0; // 0 = speed boost, 1 = jump boost, 2 = time slow
    [SerializeField] public float speed = 4.0f;
    [SerializeField] public float height = 0.1f;
    private Vector3 initial;

    public Material Crystal;
    public Material Crystal1;
    public Material Crystal2;

    int numOfChildren;

    [SerializeField] private GameObject respawnerObject;

    void Start()
    {
        numOfChildren = transform.childCount;
        spellSetup();
        initial = transform.position;
    }

    // floating motion
    void Update()
    {
        if (gameObject.activeSelf) {
            float yPos = Mathf.Sin(Time.time * speed) * height; 
            transform.position = new Vector3(initial.x, initial.y + yPos, initial.z);
        }
    }

    private void spellSetup() {
        if (type == 0) {
            for(int i = 0; i < numOfChildren; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                child.GetComponent<Renderer>().material = Crystal;
            }
        } else if (type == 1) { 
            for(int i = 0; i < numOfChildren; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                child.GetComponent<Renderer>().material = Crystal1;
            }
        } else {
            for(int i = 0; i < numOfChildren; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                child.GetComponent<Renderer>().material = Crystal2;
            }
        }
    }

    public PlayerController playerScript;
    
    //player consumes the speed boost
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            playerScript = other.GetComponent<PlayerController>();
            gameObject.SetActive(!gameObject.activeSelf);
            if (type == 0) {
                playerScript.SpeedBoostTrigger();
            } else if (type == 1) {
                playerScript.JumpBoost();
            } else {
                //time slow
            }
            Respawner respawner = respawnerObject.GetComponent<Respawner>();
            respawner.ResetTimer();
        }
    }
}
