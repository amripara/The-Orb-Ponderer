using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public float bobbingAmount = 0.05f;
    public float bobbingSpeed = 14f;
    public PlayerController player;
    private float defaultPosY = 0;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;   
    }

    // Update is called once per frame
    void Update()
    {
       if (!player.IsSliding() && player.IsGrounded() && !player.IsDead())
       timer += Time.deltaTime * bobbingSpeed;
       transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
    }
}
