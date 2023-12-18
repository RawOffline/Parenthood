using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildJumpOnParent : MonoBehaviour
{
    private Rigidbody2D rb;
    private MotherMovement motherMovement;
    private Follow follow;
    private SpriteRenderer sprite;
    RaycastHit2D hitInfo;

    [SerializeField] private LayerMask parentLayer;

    private float maxRaycastDistance = 9f;
    private float wallApproachThreshold = 4f;
    private float jumpDistanceThreshold = 0.3f;
    

    Vector2 topCenter;
    private bool hasTestBeenCalled = false;
    private float distance;


    void Start()
    {
        follow = GetComponent<Follow>();
        motherMovement = FindAnyObjectByType<MotherMovement>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {

        RayCast();


        if (motherMovement.wallCheck && motherMovement.IsGrounded() && !hasTestBeenCalled)
        {
            if (follow.movingLeft == true  && motherMovement.IsGrounded())
            {
                follow.movingLeft = false;
            }
            follow.movingRight = true;
            if (distance < wallApproachThreshold) 
            {
                follow.canJump = false;
            }
            if (distance < jumpDistanceThreshold && follow.isGrounded)
            {
                JumpOnParent();
                hasTestBeenCalled = true;
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
        if (collision.gameObject.layer == 11 && transform.position.y > follow.target.position.y && motherMovement.wallCheck)
        {
            Kill();
            float jumpForceX = sprite.flipX ? -0.5f : 0.5f;

            rb.AddForce(new Vector2(jumpForceX, 2f), ForceMode2D.Impulse);
            follow.isFollowing = false;
        }
    }

    private void JumpOnParent()
    {
        rb.velocity = Vector2.zero;
        transform.DOJump(topCenter, 0.3f, 1, 1).OnComplete(Kill);

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
