using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float moveVal;
    private Vector3 posOffset;
    private Vector3 tempPos = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        //moveVal += Time.deltaTime;
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime * 2);
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 1f) * 0.5f;
        transform.position = tempPos;
        //transform.position = transform.position + new Vector3(0, 0.025f*Mathf.Sin(moveVal*2.5f),0); 
    }
}
