using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckRadius = 0.1f;

    public Transform target;  // The target character to follow
    public float maxSpeed = 5f;  // Maximum speed of the follower
    public float acceleration = 10f;  // Acceleration rate
    public float deceleration = 4f;  // Deceleration rate
    public float stoppingDistance = 1f; // Distance at which the follower stops moving
    public float minJumpInterval = 0.5f;  // Minimum time between automatic jumps
    public float maxJumpInterval = 1.5f; // Maximum time between automatic jumps
    public float minJumpForce = 3f;  // Minimum jump force
    public float maxJumpForce = 4.5f;  // Maximum jump force

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    public bool isFollowing = false;
    private float timeSinceLastJump = 0f;
    private float jumpInterval = 0f;
    private float jumpForce;
    public bool canJump;
    public bool isGrounded;
    public bool movingRight;
    public bool movingLeft;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        SetRandomJumpInterval();
        SetRandomJumpForce();
        movingRight = false;
        movingLeft = false;
    }

    void Update()
    {

        if (movingRight)
        { ScriptedMoveRight(); }
        if (movingLeft)
        { ScriptedMoveLeft(); }



        if (Input.GetKeyDown(KeyCode.E) && !isFollowing)
        {
            if (SceneManager.GetActiveScene().name != "LevelOne")
            {
                isFollowing = true;
            }
        }

        if (isFollowing)
            FollowParent();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        GravityAdjust();

        // Increment the time since the last jump
        timeSinceLastJump += Time.deltaTime;

        if (isGrounded)
        {
            // Check if it's time for an automatic jump
            if (isFollowing && timeSinceLastJump >= jumpInterval)
            {
                // Perform the jump
                Jump();

                // Set a new random jump interval and force
                SetRandomJumpInterval();
                SetRandomJumpForce();
            }
        }
    }

    public void FollowParent()
    {
        // Calculate the direction from the follower to the target (only on the x-axis)
        Vector2 direction = new Vector2(target.position.x - transform.position.x, 0);

        // Calculate the distance to the target
        float distance = direction.magnitude;

        // Adjust speed based on distance
        if (distance > stoppingDistance)
        {
            // Accelerate up to the maximum speed
            float speedToApply = Mathf.Min(maxSpeed, rb.velocity.magnitude + acceleration * Time.deltaTime);
            var velocity = new Vector2(direction.normalized.x * speedToApply, rb.velocity.y);
            rb.velocity = velocity;

            // Flip the character sprite based on movement direction
            if (direction.x > 0)
                sprite.flipX = false;
            else if (direction.x < 0)
                sprite.flipX = true;
        }

        if (isGrounded)
        {
            // Adjust speed based on distance
            if (distance > stoppingDistance)
            {
                // Accelerate up to the maximum speed
                float speedToApply = Mathf.Min(maxSpeed, rb.velocity.magnitude + acceleration * Time.deltaTime);
                var velocity = new Vector2(direction.normalized.x * speedToApply, rb.velocity.y); 
                rb.velocity = velocity;

                // Flip the character sprite based on movement direction
                if (direction.x > 0)
                    sprite.flipX = false;
                else if (direction.x < 0)
                    sprite.flipX = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the target
        if (collision.transform == target)
        {
            // Stop following when colliding with the target
            isFollowing = false;
        }
    }

    void Jump()
    {
        if (canJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }


        // Reset the timer
        timeSinceLastJump = 0f;
    }

    void SetRandomJumpInterval()
    {
        // Set a random jump interval between minJumpInterval and maxJumpInterval
        jumpInterval = Random.Range(minJumpInterval, maxJumpInterval);
    }

    void SetRandomJumpForce()
    {
        // Set a random jump force between minJumpForce and maxJumpForce
        jumpForce = Random.Range(minJumpForce, maxJumpForce);
    }

    private void GravityAdjust()
    {
        //if (rb.velocity.y < 0)
        //    rb.gravityScale = 2.5f;

        //else if (isGrounded)
        //    rb.gravityScale = 1;
    }

    private void ScriptedMoveRight()
    {
        sprite.flipX = false;
        if (!isFollowing)
        {
            if (timeSinceLastJump >= jumpInterval)
            {
                Jump();

                SetRandomJumpInterval();
                SetRandomJumpForce();
            }
            isFollowing = false;

            float targetSpeed = Mathf.Abs(transform.position.x - target.position.x) > 2.5f ? 0.2f : maxSpeed;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetSpeed, 1f * Time.deltaTime), rb.velocity.y);
        }
    }

    private void ScriptedMoveLeft()
    {
        sprite.flipX = true;
        if (!isFollowing)
        {
            if (timeSinceLastJump >= jumpInterval)
            {
                Jump();

                SetRandomJumpInterval();
                SetRandomJumpForce();
            }
            isFollowing = false;

            float targetSpeed = Mathf.Abs(transform.position.x - target.position.x) > 2.5f ? -0.2f : -maxSpeed;
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetSpeed, 1f * Time.deltaTime), rb.velocity.y);
        }
    }


}
