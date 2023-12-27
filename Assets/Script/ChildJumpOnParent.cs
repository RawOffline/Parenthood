using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildJumpOnParent : MonoBehaviour
{
    private Rigidbody2D rb;
    private MotherMovement motherMovement;
    private ChildJumpOverSmall childJumpOverSmall;
    private Follow follow;
    private SpriteRenderer sprite;
    RaycastHit2D hitInfo;

    [SerializeField] private LayerMask parentLayer;

    private float maxRaycastDistance = 9f;
    private float jumpDistanceThreshold = 0.2f;
    

    Vector2 topCenter;
    private bool hasTestBeenCalled = false;
    private float distance;
    private float originalStoppingDistance;


    void Start()
    {
        follow = GetComponent<Follow>();
        motherMovement = FindAnyObjectByType<MotherMovement>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        childJumpOverSmall = GetComponent<ChildJumpOverSmall>();
        originalStoppingDistance = follow.stoppingDistance;
    }

    void Update()
    {

        RayCast();

        if (motherMovement.wallCheck)
        {
            follow.canJump = false;
            follow.movingLeft = false;
            follow.movingRight = false;
            if (motherMovement.IsGrounded() && !hasTestBeenCalled)
            {
                follow.stoppingDistance = 0f;
                follow.isFollowing = true;
                if (distance < jumpDistanceThreshold && follow.isGrounded)
                {
                    JumpOnParent();
                    follow.isFollowing = false;
                    hasTestBeenCalled = true;
                }
            }
        }
        else if (childJumpOverSmall.smallStepRay == false)
        {
            follow.canJump = true;
        }

        if (!motherMovement.wallCheck && hasTestBeenCalled)
        {
            Kill();
            hasTestBeenCalled = false;
        }

    }

    private void RayCast()
    {
        hitInfo = Physics2D.Raycast(transform.position, new Vector2((sprite.flipX ? -1f : 1f), 0), maxRaycastDistance, parentLayer);
        var parentCollider = hitInfo.collider;
        distance = Mathf.Abs(hitInfo.point.x - transform.position.x);
        if (parentCollider != null)
        {
            topCenter = new Vector2(parentCollider.bounds.center.x, parentCollider.bounds.max.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.layer == 11)
        {
            follow.isFollowing = false;
            follow.stoppingDistance = originalStoppingDistance;
            if (transform.position.y > follow.target.position.y && motherMovement.wallCheck)
            {
                Invoke("Timer", 2);
                Kill();
                rb.velocity = Vector2.zero;
                float jumpForceX = sprite.flipX ? -0.5f : 0.5f;
                rb.AddForce(new Vector2(jumpForceX, 4f), ForceMode2D.Impulse);
            }
        }
    }

    private void JumpOnParent()
    {
        rb.velocity = Vector2.zero;
        transform.DOJump(topCenter, 0.3f, 1, 1).OnComplete(Kill);

    }

    private void Timer()
    {
        hasTestBeenCalled = false;
    }
    private void OnDisable()
    {
        Kill();
    }

    private void Kill()
    {
        transform.DOKill();
    }
}
