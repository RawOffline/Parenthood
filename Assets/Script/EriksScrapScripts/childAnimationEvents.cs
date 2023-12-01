using UnityEngine;
using static ChildAnimationEvents;

public class ChildAnimationEvents : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float jumpForce = 8f;
    private float acceleration = 0.5f;
    public LayerMask childLayer;
    public LayerMask parentLayer;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Follow follow;
    private float currentVelocity = 0.0f;
    public enum childState
    {
        Stop,
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
    }

    void Update()
    {
        isGrounded = follow.isGrounded;

        switch (currentState)
        {
            case childState.Stop:
                IgnoreCollisionBetweenLayers(childLayer, parentLayer, true);
                Stop();
                break;

            case childState.Idle:
                IgnoreCollisionBetweenLayers(childLayer, parentLayer, false);
                break;

            case childState.MovingRight:
                
                IgnoreCollisionBetweenLayers(childLayer, parentLayer, true);
                targetVelocityX = moveSpeed;
                Move();
                break;

            case childState.MovingLeft:
                IgnoreCollisionBetweenLayers(childLayer, parentLayer, true);
                targetVelocityX = -moveSpeed;
                Move();
                break;

            case childState.Jumping:
                Jump();
                break;

            case childState.JumpRight:
                IgnoreCollisionBetweenLayers(childLayer, parentLayer, true);
                targetVelocityX = moveSpeed;
                Jump();
                break;

            case childState.JumpLeft:
                IgnoreCollisionBetweenLayers(childLayer, parentLayer, true);
                targetVelocityX = -moveSpeed;
                Jump();
                break;
        }
    }

    public void ResetCollisionState()
    {
        IgnoreCollisionBetweenLayers(childLayer, parentLayer, false);
    }

    void IgnoreCollisionBetweenLayers(LayerMask layer1, LayerMask layer2, bool ignore)
    {
        Collider2D[] colliders1 = Physics2D.OverlapCircleAll(transform.position, 100f, layer1);
        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(transform.position, 100f, layer2);

        foreach (Collider2D collider1 in colliders1)
        {
            foreach (Collider2D collider2 in colliders2)
            {
                Physics2D.IgnoreCollision(collider1, collider2, ignore);
            }
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

    void Stop()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
