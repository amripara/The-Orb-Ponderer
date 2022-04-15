using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    private GameObject camera;
    private AudioSource camAudio;
    [SerializeField] GameObject hand;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;
    private bool reachedPos1 = false;
    private bool reachedPos2 = false;
    [SerializeField] private float moveSpeed = 1f;

    private float startTime, pos2Time;
    [SerializeField] private float waitTimer;
    private float jour1Length, jour2Length, handMoveLength;

    [SerializeField] private Vector3 handPos2;
    private Quaternion handRot1;

    [SerializeField] private GameObject winController;

    private void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        startPos = camera.transform.position;
        startTime = Time.time;
        jour1Length = Vector3.Distance(startPos, pos1);
        jour2Length = Vector3.Distance(pos1,pos2);
        handRot1 = hand.transform.rotation;
        //handMoveLength = Vector3.Distance(handPos1, handPos2);
        camAudio = camera.GetComponent<AudioSource>();
        //winController = GameObject.Find("WinScreenController");
    }
    // Start is called before the first frame update
    void Start()
    {
        camAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedPos1)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / jour1Length;
            camera.transform.position = Vector3.Lerp(startPos, pos1, fractionOfJourney);
        }
        else if (!reachedPos2 && waitTimer < 0)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / jour2Length;
            camera.transform.position = Vector3.Lerp(pos1, pos2, fractionOfJourney);
        }
        else if (reachedPos2) {
            float distCovered = (Time.time - pos2Time) * moveSpeed*2;
            float fractionOfJourney = distCovered / jour1Length;
            hand.transform.rotation = Quaternion.Lerp(handRot1, Quaternion.LookRotation(handPos2,Vector3.up), fractionOfJourney);
        }
        else if (reachedPos1)
        {
            waitTimer -= Time.deltaTime;
        }

        if (camera.transform.position == pos1)
        {
            reachedPos1 = true;
            startTime = Time.time;
            if (waitTimer == 0)
            {
                waitTimer = 2;
            }
        } else if (camera.transform.position == pos2)
        {
            if (pos2Time == 0)
            {
                pos2Time = Time.time;
            }
            reachedPos2 = true;
        }

        if(hand.transform.rotation == Quaternion.LookRotation(handPos2, Vector3.up))
        {
            winController.SetActive(true);
        }
    }
}
