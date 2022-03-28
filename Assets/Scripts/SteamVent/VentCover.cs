using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentCover : MonoBehaviour
{
    private int angle = 60;

    public void open() {
        transform.RotateAround(transform.position, -Vector3.right, angle);
    }

    public void close() {
        transform.RotateAround(transform.position, Vector3.right, angle);
    }
}
