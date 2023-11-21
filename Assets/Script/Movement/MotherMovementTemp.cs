using System.Collections;
using UnityEngine;

public class MotherMovementTemp : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float airLinearDrag = 2.5f;
    [SerializeField] float fallMultiplier = 8f;
    [SerializeField] float lowJumpFallMultiplier = 5f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 27f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;

    [Header("Grounchecking")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public float groundCheckDistance;
    public Vector2 groundCheckBoxSize;

    [Header("Movement")]
    [SerializeField] float movementAcceleration;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float movementDeaccleration;
    private float horizontalDir;

    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private TrailRenderer tr;

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontalDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }


        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
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

        if (!isDashing)
        {
            FallMultiplier();
            ApplyAirLinearDrag();
        }
    }



    private bool IsGrounded()
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
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpFallMultiplier;
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}