using System.Collections;
using UnityEngine;

public class MotherMovement : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] float airLinearDrag = 2.5f;
    [SerializeField] float fallMultiplier = 8f;
    [SerializeField] float lowJumpFallMultiplier = 5f;
    [SerializeField] float jumpingPower = 16f;
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.3f;
    private float jumBufferCounter;
 
    [Header("Dash")]
    private bool canDash = true;
    public bool isDashing;
    private float dashingPower = 40f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 0.5f;

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
    //[SerializeField] private TrailRenderer tr;

    private void Start()
    {
       // Time.fixedDeltaTime = Time.deltaTime * 2f;
    }

    private void Update()
    {
        WallCheck();
        if (isDashing)
        {
            return;
        }

        horizontalDir = Input.GetAxisRaw("Horizontal");

        if (jumBufferCounter >0f && coyoteTimeCounter > 0f)
        {
            Jump();
        }

        if(Input.GetButtonDown("Jump"))
        {
            jumBufferCounter = jumpBufferTime;
        }
        else
        {
            jumBufferCounter -= Time.deltaTime;
        }


        //if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        //{
        //    StartCoroutine(Dash());
        //}

        if(IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter-= Time.deltaTime; 
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Movement();

        if (!isDashing)
        {
            FallMultiplier();
        }
    }

    private void Movement()
    {
        float playerVelocity = horizontalDir * maxMoveSpeed;
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, playerVelocity, movementAcceleration * Time.fixedDeltaTime), rb.velocity.y);
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
        if (rb.velocity.y < 4f)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 2f && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpFallMultiplier;

            coyoteTimeCounter = 0f;
        }
        else
        {
            rb.gravityScale = 1f;
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