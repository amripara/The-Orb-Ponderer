using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAnimation : MonoBehaviour
{
    public Vector3 startPos;
    public float displacement = 0.1f;
    public float hoverSpeed = 1.0f;

    private void Start() {
        startPos = this.gameObject.transform.position;
    }
    private void Update() {
        this.gameObject.transform.position = startPos + new Vector3(0, displacement * Mathf.Sin(Mathf.PI * hoverSpeed * Time.time));
    }
}
