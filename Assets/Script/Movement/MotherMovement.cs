using System.Collections;
using UnityEngine;

public class MotherMovement : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] float airLinearDrag = 2.5f;
    [SerializeField] float fallMultiplier = 8f;
    [SerializeField] float lowJumpFallMultiplier = 5f;
    [SerializeField] float jumpingPower = 12f;
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.3f;
    private float jumBufferCounter;

    [Header("Dash")]
    //private bool canDash = true;
    public bool isDashing;
    //private float dashingPower = 40f;
    //private float dashingTime = 0.1f;
    //private float dashingCooldown = 0.5f;

    [Header("Grounchecking")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public float groundCheckDistance;
    public Vector2 groundCheckBoxSize;

    [Header("Movement")]
    [SerializeField] float movementAcceleration = 20;
    [SerializeField] float maxMoveSpeed = 7;
    [SerializeField] float movementDeaccleration = 3;
    private float horizontalDir;
    private bool isFacingRight = true;
    public bool wallCheck = false;
    private float wallCheckTimer;
    private float wallCheckThreshold = 1.0f;
    [SerializeField] private Rigidbody2D rb;
    private bool isGodMode = false;
    // private bool isJumping = false;
    //Animator motherAnimation;

    private void Start()
    {
        // Time.fixedDeltaTime = Time.deltaTime * 2f;
        //motherAnimation = GetComponent<Animator>();
    }

    private void Update()
    {
        WallCheck();
        if (isDashing)
        {
            return;
        }

        horizontalDir = Input.GetAxisRaw("Horizontal");

        if (jumBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumBufferCounter = jumpBufferTime;
            //isJumping = true;
            //motherAnimation.SetBool("isJumping", true);
        }
        else
        {
            jumBufferCounter -= Time.deltaTime;
        }


        //if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        //{
        //    StartCoroutine(Dash());
        //}

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }


        if (!isDashing)
        {
            FallMultiplier();
        }

        Movement();

    }

    private void Movement()
    {
        float playerVelocity = horizontalDir * maxMoveSpeed;
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, playerVelocity, movementAcceleration * Time.fixedDeltaTime), rb.velocity.y);
        //motherAnimation.SetBool("isWalking", true);

        //if (rb.velocity.y < 0.1)
        //{
        //    motherAnimation.SetBool("isIdle", true);

        //}

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        jumBufferCounter = 0f;

        if (!isDashing)
        {
            FallMultiplier();
            ApplyAirLinearDrag();
        }
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
    private void ApplyGoundLinearDrag()
    {
        if (Mathf.Abs(horizontalDir) < 0.4f || isFacingRight)
        {
            rb.drag = movementDeaccleration;
        }
        else
        {
            rb.drag = 0f;
        }
    }
    private void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
    }

    private void FallMultiplier()
    {
        if (rb.velocity.y < 4f && !isGodMode)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 2f && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpFallMultiplier;

            coyoteTimeCounter = 0f;
        }

        if (IsGrounded())
        {
            rb.gravityScale = 1f;
        }

        if (isGodMode)
        {
            rb.gravityScale = 0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckDistance, groundCheckBoxSize);
    }

    private void Flip()
    {
        if (isFacingRight && horizontalDir < 0f || !isFacingRight && horizontalDir > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    private void WallCheck()
    {
        if (Physics2D.BoxCast(transform.position, new Vector2(0.15f, 0.15f), 0, transform.right, 0.5f, groundLayer) ||
            Physics2D.BoxCast(transform.position, new Vector2(0.15f, 0.15f), 0, -transform.right, 0.5f, groundLayer))
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

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            // Transform the player's position to the platform's position
            //transform.position = other.transform.position;

            // Set the player as a child of the platform
            transform.SetParent(other.transform);

            // Optionally, disable player's gravity and apply the platform's movement
            // This assumes the moving platform has a rigidbody
            Rigidbody2D platformRb = other.GetComponent<Rigidbody2D>();
            if (platformRb != null)
            {
                rb.velocity = platformRb.velocity;
                rb.gravityScale = 10f; // You might need to adjust this based on your game's requirements
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            // Detach the player from the platform
            transform.SetParent(null);

            // Restore player's gravity
            rb.gravityScale = 1; // You might need to adjust this based on your game's requirements
        }
    }

    //private IEnumerator Dash()
    //{
    //    canDash = false;
    //    isDashing = true;
    //    float originalGravity = rb.gravityScale;
    //    rb.gravityScale = 0.5f;
    //    rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
    //    yield return new WaitForSeconds(dashingTime);
    //    rb.gravityScale = originalGravity;
    //    isDashing = false;
    //    yield return new WaitForSeconds(dashingCooldown);
    //    canDash = true;
    //}
}