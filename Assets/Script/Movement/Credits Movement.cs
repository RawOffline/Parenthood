using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreditsMovement : MonoBehaviour
{
    [Header("Groundchecking")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask parentJumpStepLayer;
    public float groundCheckDistance;
    public Vector2 groundCheckBoxSize;

    [Header("Movement")]
    [SerializeField] float movementAcceleration = 20;
    [SerializeField] float maxMoveSpeed = 7;
    [SerializeField] private Rigidbody2D rb;
    [HideInInspector] public GameObject platform;
    private float horizontalInput;
    public bool isGodMode = false;

    public enum Directions { None, Left, Right };
    public Directions directions;
    public float lockedDirection;
    Animator motherAnimation;
    private bool hasLanded = true;
    public UnityEvent LandingTrigger;
    public UnityEvent JumpingTrigger;
    //public bool onPlatfrom = false;

    // TEST CODE
    float velocityX;
    public bool onConveyer;

    private void Start()
    {
        motherAnimation = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (platform != null && platform.transform.parent != null)
        {
            rb.velocity += platform.GetComponent<Rigidbody2D>().velocity;
        }
        if (!onConveyer)
        {
            Movement();
        }
        else
        {
            Vector2 movement = new Vector2(horizontalInput, 0f);
            transform.Translate(movement * maxMoveSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        horizontalInput = 0;
        CheckInputs();
        SetInput();
    }

    private void SetInput()
    {
        if (horizontalInput == 0)
        {
            lockedDirection = 0;
        }

        directions = Directions.None;
        if (horizontalInput < 0)
        {
            directions = Directions.Left;
        }
        else if (horizontalInput > 0)
        {
            directions = Directions.Right;
        }
    }
    private void CheckInputs()
    {
        /*if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (directions == Directions.Right && lockedDirection == 0)
            {
                lockedDirection = -1;
            }
            horizontalInput = -1;
        }*/

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (directions == Directions.Left && lockedDirection == 0)
            {
                lockedDirection = 1;
            }
            horizontalInput = 1;
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            lockedDirection = 0;
        }

        /*if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            lockedDirection = 0;
        }*/
    }


    private void Movement()
    {
        if (lockedDirection != 0)
        {
            horizontalInput = lockedDirection;
        }

        rb.velocity = new Vector2(horizontalInput * maxMoveSpeed, rb.velocity.y);
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, horizontalInput,
            movementAcceleration * Time.fixedDeltaTime), rb.velocity.y);

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            motherAnimation.SetBool("isWalking", true);
        }

        if (Mathf.Abs(horizontalInput) < 0.1f)
        {
            motherAnimation.SetBool("isWalking", false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckDistance, groundCheckBoxSize);
        Gizmos.color = Color.red;
    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, groundCheckBoxSize, 0, -transform.up, groundCheckDistance, groundLayer))
        {
            if (hasLanded)
            {
                hasLanded = false;
                LandingTrigger.Invoke();
            }
            return true;
        }
        else
        {
            hasLanded = true;
            return false;
        }
    }

    public void EnableGodMode()
    {
        GetComponent<Collider2D>().enabled = false;
        rb.gravityScale = 0;
        isGodMode = true;
    }

    public void DisableGodMode()
    {
        GetComponent<Collider2D>().enabled = true;
        rb.gravityScale = 1f;
        isGodMode = false;
    }

    public void HandleGodModeMovement()
    {
        if (isGodMode)
        {
            float moveSpeed = 10f;

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f);
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            rb.gravityScale = 0f;
        }
    }
}
