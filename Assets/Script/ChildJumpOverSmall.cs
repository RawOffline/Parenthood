using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class ChildJumpOverSmall : MonoBehaviour
{
    Follow follow;
    Rigidbody2D rb;
    private SpriteRenderer sprite;
    [SerializeField] private LayerMask groundLayer;
    private float maxRaycastDistance = 7f;
    private float wallApproachThreshold = 4f;
    private float jumpDistanceThreshold = 0.1f;

    public float xForce = 0.1f;
    public float yForce = 0.2f;

    RaycastHit2D hitInfo;
    void Start()
    {
        follow = GetComponent<Follow>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {   
        RayCast();

        if (hitInfo.collider != null)
        {
            Debug.Log("hit");
            float distance = Mathf.Abs(hitInfo.point.x - transform.position.x);
            follow.canJump = false;


            if (distance < wallApproachThreshold)
            {
                follow.maxSpeed = distance + 0.5f;
            }

            if (distance < jumpDistanceThreshold && follow.isGrounded)
            {
                follow.maxSpeed = 0.5f;
                Jump();

            }

        }

        else
        {
            follow.canJump = true;
            follow.maxSpeed = 2.5f;
        }
    }

    private void RayCast()
    {
        if (sprite.flipX == true)
        {
            hitInfo = Physics2D.Raycast(transform.position, new Vector2(-1f, 0), maxRaycastDistance, groundLayer);
        }
        else
        {
            hitInfo = Physics2D.Raycast(transform.position, new Vector2(1f, 0), maxRaycastDistance, groundLayer);
        }
        
    }

    private void Jump()
    {
        if (sprite.flipX == true)
        {
            rb.AddForce(new Vector2(-xForce, yForce), ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        }

    }
}
