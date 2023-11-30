using UnityEngine;
using static ChildAnimationEvents;

public class ChildAnimationEvents : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Follow follow;
    
    // Enum to represent different states
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

    void Start()
    {
        follow = GetComponent<Follow>();
        rb = GetComponent<Rigidbody2D>();
        currentState = childState.Idle;
    }

    void Update()
    {
        isGrounded = follow.isGrounded;
        
        //if (currentState != childState.Idle)
        //{
        //    follow.enabled = false;
        //}
        //else
        //{
        //    follow.enabled = true;
        //}
        switch (currentState)
        {
            case childState.Idle:
                Idle();
                break;

            case childState.MovingRight:
                MoveRight();
                break;

            case childState.MovingLeft:
                MoveLeft();
                break;

            case childState.Jumping:
                Jump();
                break;

            case childState.JumpRight:
                JumpRight();
                break;

            case childState.JumpLeft:
                JumpLeft();
                break;
        }
    }


    public void ChangeState(childState newState)
    {
        currentState = newState;
    }

    void MoveRight()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    void MoveLeft()
    {
        rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
    }

    void JumpLeft()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(-moveSpeed, jumpForce);
        }
    }
    void JumpRight()
    {

        if (isGrounded)
        {
            rb.velocity = new Vector2(moveSpeed, jumpForce);
        }
    }


    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(0, jumpForce);
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
