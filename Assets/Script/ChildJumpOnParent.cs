using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildJumpOnParent : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool reachedDestination = true;
    private MotherMovement motherMovement;
    private Follow follow;
    private SpriteRenderer sprite;
    RaycastHit2D hitInfo;

    [SerializeField] private LayerMask parentLayer;

    private float maxRaycastDistance = 9f;
    private float wallApproachThreshold = 4f;
    private float jumpDistanceThreshold = 0.3f;

    public float xForce = 0.1f;
    public float yForce = 0.2f;

    void Start()
    {
        follow = GetComponent<Follow>();
        motherMovement = FindAnyObjectByType<MotherMovement>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        isGrounded = follow.isGrounded;

        RayCast();


        if (motherMovement.wallCheck == true && motherMovement.IsGrounded() && transform.position.y < follow.target.position.y && reachedDestination)
        {
            JumpOnParent();
        }

        else
        {
            follow.canJump = true;
            follow.maxSpeed = 2.5f;
        }
    }

    private void RayCast()
    {
        hitInfo = Physics2D.Raycast(transform.position, new Vector2((sprite.flipX ? -1f : 1f), 0), maxRaycastDistance, parentLayer);
    }

    private void JumpOnParent()
    {
        follow.canJump = false;

        float distance = Mathf.Abs(hitInfo.point.x - transform.position.x);

        if (distance < wallApproachThreshold)
        {
            follow.maxSpeed = distance;

            if (distance < jumpDistanceThreshold && follow.isGrounded)
            {
                follow.maxSpeed = 0.5f;
                float jumpForceX = sprite.flipX ? -xForce : xForce;
                rb.AddForce(new Vector2(jumpForceX, yForce), ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11 && transform.position.y > follow.target.position.y && motherMovement.wallCheck)
        {
            StartCoroutine(JumpFromParent());
        }
    }

    private IEnumerator JumpFromParent()
    {
        float jumpForceX = sprite.flipX ? -0.7f : 0.7f;

        rb.AddForce(new Vector2(jumpForceX, 2f), ForceMode2D.Impulse);

        reachedDestination = false;
        yield return new WaitForSeconds(1);
        reachedDestination = true;
    }

}
