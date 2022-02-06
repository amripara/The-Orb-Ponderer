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
    public bool IsDead { get => isDead; }
    [SerializeField] private bool isDead;

    //Moving forward, jumping, sliding
    public float movementSpeed;
    public float speedThreshold;
    public float slidingSpeed;

    public bool IsSliding { get => isSliding; }
    [SerializeField] private bool isSliding;

    public bool IsGrounded { get => isGrounded; }
    [SerializeField] private bool isGrounded;

    private float originalHeight;
    public float slidingHeight;

    //Turning variables
    public float turnDuration;
    private float timeRemaining;
    private bool isTurning;
    private Quaternion toDirection;
    private Quaternion fromDirection;

    // Spells
    [Header("Speed Boost")]
    [SerializeField] private float speedBoostDuration;
    [SerializeField] private float speedBoostSpeedMultiplier;
    [SerializeField] private bool speedBoostIsActive;

    [Header("Jump Boost")]
    [SerializeField] private float jumpBoostDuration;
    [SerializeField] private float jumpBoostForceMultiplier;
    [SerializeField] private bool jumpBoostIsActive;

    // Exposed movement variables
    public Vector3 velocity { get { return rb.velocity; } }
    public float speed { get { return velocity.magnitude; } }

    public static PlayerController Instance { get { return _instance; } }
    private static PlayerController _instance;

    private void Awake()
    {
        _instance = this;
    }

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
            if (playerInput.actions["Jump"].WasPressedThisFrame() && isGrounded) // Jumping
            {
                if (jumpBoostIsActive)
                {
                    rb.AddForce(Vector3.up * 5 * jumpBoostForceMultiplier, ForceMode.VelocityChange);
                } else
                {
                    rb.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
                }
                isGrounded = false;
            }
            if (transform.position.y < 0)
            {
                loseTextObject.SetActive(true);
                isDead = true;
            }
            if (playerInput.actions["Slide"].WasPerformedThisFrame() && isGrounded && !isSliding)
            {
                isSliding = true;
                capsule.height = slidingHeight;
                rb.AddForce(transform.forward * slidingSpeed, ForceMode.VelocityChange);
            }
            // Spells
            if (playerInput.actions["SpeedBoost"].WasPerformedThisFrame())
            {
                StartCoroutine(SpeedBoost());
            }
            if (playerInput.actions["JumpBoost"].WasPerformedThisFrame())
            {
                StartCoroutine(JumpBoost());
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
        if (!isTurning && isGrounded && !isSliding && !isDead)
        {
            if (rb.velocity.magnitude < speedThreshold)
            {
                if (speedBoostIsActive)
                {
                    rb.velocity = transform.forward * movementSpeed * speedBoostSpeedMultiplier;
                } else
                {
                    rb.velocity = transform.forward * movementSpeed;
                }
            }
        }
        if (isSliding)
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
            isGrounded = true;
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
        if (other.gameObject.CompareTag("Obstacle") && !isSliding)
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
        isSliding = false;
    }

    /// <summary>
    /// Applies a speed buff to the player for a duration of speedBoostDuration.
    /// This simply toggles speedBoostIsActive. FixedUpdate accounts for this to decide
    /// how quickly the player moves.
    /// </summary>
    private IEnumerator SpeedBoost()
    {
        speedBoostIsActive = true;
        yield return new WaitForSeconds(speedBoostDuration);
        speedBoostIsActive = false;
    }
    /// <summary>
    /// Operates identically to the SpeedBoost method.
    /// </summary>
    private IEnumerator JumpBoost()
    {
        jumpBoostIsActive = true;
        yield return new WaitForSeconds(jumpBoostDuration);
        jumpBoostIsActive = false;
    }

}
