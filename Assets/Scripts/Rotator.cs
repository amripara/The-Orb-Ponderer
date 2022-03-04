using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float moveVal;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        moveVal += Time.deltaTime;
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime * 2);
        transform.position = transform.position + new Vector3(0, 0.025f*Mathf.Sin(moveVal*2.5f),0); 
    }
}
