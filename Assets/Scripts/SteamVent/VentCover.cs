using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentCover : MonoBehaviour
{
    private int angle = 70;

    public void open() {
        transform.RotateAround(transform.position, Vector3.forward, angle);
    }

    public void close() {
        transform.RotateAround(transform.position, -Vector3.forward, angle);
    }
}
