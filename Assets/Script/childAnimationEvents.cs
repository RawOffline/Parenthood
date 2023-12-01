using UnityEngine;
using static ChildAnimationEvents;

public class ChildAnimationEvents : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpForce = 8f;
    private float acceleration = 0.5f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Follow follow;
    private float currentVelocity = 0.0f;
    public enum childState
    {
        Idle,
        MovingRight,
        MovingLeft,
        Jumping,
        JumpLeft,
        JumpRight,
    }

    private childState currentState;

    private float targetVelocityX;

    void Start()
    {
        follow = GetComponent<Follow>();
        rb = GetComponent<Rigidbody2D>();
        currentState = childState.Idle;
    }

    void Update()
    {
        isGrounded = follow.isGrounded;

        switch (currentState)
        {
            case childState.Idle:
                Idle();
                break;

            case childState.MovingRight:
                targetVelocityX = moveSpeed;
                Move();
                break;

            case childState.MovingLeft:
                targetVelocityX = -moveSpeed;
                Move();
                break;

            case childState.Jumping:
                Jump();
                break;

            case childState.JumpRight:
                targetVelocityX = moveSpeed;
                Jump();
                break;

            case childState.JumpLeft:
                targetVelocityX = -moveSpeed;
                Jump();
                break;
        }
    }

    public void ChangeState(childState newState)
    {
        currentState = newState;
    }

    void Move()
    {
        rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref currentVelocity, acceleration), rb.velocity.y);

    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void Idle()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
