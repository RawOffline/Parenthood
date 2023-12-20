using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MotherMovement : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] float jumpingPower = 12f;
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private bool isJumping;

    [Header("Groundchecking")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask parentJumpStepLayer;
    public float groundCheckDistance;
    public Vector2 groundCheckBoxSize;

    [Header("WallChecking")]
    public bool wallCheck = false;
    private float wallCheckTimer;
    private float wallCheckThreshold = 1.0f;

    [Header("Movement")]
    [SerializeField] float movementAcceleration = 20;
    [SerializeField] float maxMoveSpeed = 7;
    //[SerializeField] float movementDeaccleration = 3;
    [SerializeField] private Rigidbody2D rb;
    [HideInInspector] public GameObject platform;
    private float horizontalInput;
    private bool isFacingRight = true;
    public bool isGodMode = false;

    public enum Directions { None, Left, Right };
    public Directions directions;
    public float lockedDirection;
    Animator motherAnimation;
    //public bool onPlatfrom = false;

    // TEST CODE
    float velocityX;

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
        Movement();
    }

    private void Update()
    {
        WallCheck();
        horizontalInput = 0;
        CheckInputs();
        SetInput();

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
        UpdateJumpBuffer();

        UpdateCoyoteTime();

        Flip();
    }

    private void UpdateJumpBuffer()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;

        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void UpdateCoyoteTime()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (directions == Directions.Right && lockedDirection == 0)
            {
                lockedDirection = -1;
            }
            horizontalInput = -1;
        }

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
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            lockedDirection = 0;
        }
    }


    private void Movement()
    {
        if (lockedDirection != 0)
        {
            horizontalInput = lockedDirection;
        }

        rb.velocity = new Vector2(horizontalInput * maxMoveSpeed, rb.velocity.y);
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, horizontalInput, movementAcceleration * Time.fixedDeltaTime), rb.velocity.y);


        ////print("horizontalInput: " + horizontalInput);

        //velocityX += horizontalInput * movementAcceleration * Time.deltaTime;
        ////print("velocityX: " + velocityX);

        //velocityX = Mathf.Clamp(velocityX, -maxMoveSpeed, maxMoveSpeed);
        ////print("velocityX after clamp: " + velocityX);

        //rb.velocity = new Vector2 (velocityX, rb.velocity.y);
        ////print("rb.velocity: " + rb.velocity);



        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            motherAnimation.SetBool("isWalking", true);
        }

        if (Mathf.Abs(horizontalInput) < 0.1f)
        {
            motherAnimation.SetBool("isWalking", false);
        }

    }


    private void Jump()
    {
        //if (onPlatfrom)
        //{
        //    ParentHandler.Instance.RemoveParent();
        //}



        rb.gravityScale = 1;
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        jumpBufferCounter = 0f;
    }



    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckDistance, groundCheckBoxSize);
    }
    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, groundCheckBoxSize, 0, -transform.up, groundCheckDistance, groundLayer))
        {

            return true;
        }
        else
        {
            return false;
        }
    }
    private void Flip()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private void WallCheck()
    {
        if (Physics2D.BoxCast(transform.position, new Vector2(0.11f, 0.15f), 0, transform.right, 0.2f, parentJumpStepLayer) ||
            Physics2D.BoxCast(transform.position, new Vector2(0.11f, 0.15f), 0, -transform.right, 0.2f, parentJumpStepLayer))
        {
            wallCheckTimer += Time.deltaTime;


            if (wallCheckTimer >= wallCheckThreshold)
            {
                wallCheck = true;
            }
        }
        else
        {
            wallCheck = false;
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