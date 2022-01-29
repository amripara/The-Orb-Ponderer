using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInput playerInput;
    private Rigidbody rb;
    private CapsuleCollider capsule;

    //Key Pieces
    private int count;
    public GameObject winTextObject;

    //Death
    public GameObject loseTextObject;
    private bool isDead = false;

    //Moving forward, jumping, sliding
    public float movementSpeed;
    public float speedThreshold;
    public float slidingSpeed;
    private bool sliding;
    private bool grounded;
    private float originalHeight;
    public float slidingHeight;

    //Turning variables
    public float turnDuration;
    private float timeRemaining;
    private bool isTurning;
    private Quaternion toDirection;
    private Quaternion fromDirection;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        count = 0;
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        originalHeight = capsule.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (playerInput.actions["Left"].WasPerformedThisFrame() && !isTurning)
            {
                fromDirection = transform.rotation;
                toDirection = Quaternion.FromToRotation(Vector3.right, transform.forward);
                isTurning = true;
                timeRemaining = turnDuration;
            }
            if (playerInput.actions["Right"].WasPerformedThisFrame() && !isTurning)
            {
                fromDirection = transform.rotation;
                toDirection = Quaternion.FromToRotation(Vector3.left, transform.forward); //Opposite direciton but it works for some reason lmao.
                isTurning = true;
                timeRemaining = turnDuration;
            }
            if (playerInput.actions["Jump"].WasPressedThisFrame() && grounded) // Jumping
            {
                rb.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
                grounded = false;
            }
            if (transform.position.y < 0)
            {
                loseTextObject.SetActive(true);
                isDead = true;
            }
            if (playerInput.actions["Slide"].WasPerformedThisFrame() && grounded && !sliding)
            {
                sliding = true;
                capsule.height = slidingHeight;
                rb.AddForce(transform.forward * slidingSpeed, ForceMode.VelocityChange);
            }
        }  
    }

    private void FixedUpdate()
    {
        if (isTurning)
        {
            if (timeRemaining <= 0f)
            {
                isTurning = false;
            }
            timeRemaining -= Time.deltaTime;
            transform.rotation = Quaternion.Lerp(fromDirection, toDirection, (turnDuration - timeRemaining) / turnDuration);
        }
        if (!isTurning && grounded && !sliding && !isDead)
        {
            if (rb.velocity.magnitude < speedThreshold)
            {
                rb.velocity = transform.forward * movementSpeed;
            }
        }
        if (sliding)
        {
            if (rb.velocity.magnitude < speedThreshold*0.6)
            {
                UnSlide();   
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KeyStone"))
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("KeyPiece"))
        {
            other.gameObject.SetActive(false);
            count++;
            CheckCount();
        }
        if (other.gameObject.CompareTag("Obstacle") && !sliding)
        {
            loseTextObject.SetActive(true);
            Debug.Log("bonk");
            isDead = true;
        }
    }

    private void CheckCount()
    {
        if (count >= 3)
        {
            winTextObject.SetActive(true);
        }
    }

    private void UnSlide()
    {
        capsule.height = originalHeight;
        sliding = false;
    }

    public bool IsGrounded()
    {
        return grounded;
    }

    public bool IsSliding()
    {
        return sliding;
    }

    public bool IsDead()
    {
        return isDead;
    }

}
