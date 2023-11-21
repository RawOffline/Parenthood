using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherMovement : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float airLinearDrag = 2.5f;
    [SerializeField] float fallMultiplier = 8f;
    [SerializeField] float lowJumpFallMultiplier = 5f;
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
    [SerializeField] float groundLinearDrag;
    private float horizontalDir;

    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private TrailRenderer tr;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Movement();

    }
    private void Movement()
    {
        float playerVelocityX = horizontalDir * maxMoveSpeed;
        rb.velocity = new Vector2(Mathf.MoveTowards(rb.velocity.x, playerVelocityX, movementAcceleration * Time.deltaTime), rb.velocity.y);


        if (horizontalDir == 0 || (horizontalDir < 0 == playerVelocityX > 0))
        {
            rb.velocity *= 1 - groundLinearDrag * Time.deltaTime;   
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

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckDistance, groundCheckBoxSize);
    }

}
