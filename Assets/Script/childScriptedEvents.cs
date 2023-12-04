using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ChildScriptedEvents : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool reachedDestination = true;
    public LayerMask childLayer;
    public LayerMask parentLayer;

    private MotherMovement motherMovement;
    private Follow follow;
    private SpriteRenderer sprite;

    
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

        if (motherMovement.wallCheck == true && reachedDestination && motherMovement.IsGrounded())
        {
            
            ParentAgainstWall();
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


    public void ParentAgainstWall()
    {
       follow.FollowParent();
       JumpOnParent();
    }

    public void JumpOnParent()
    {
        Vector2 direction = new Vector2(follow.target.position.x - transform.position.x, 0);

        float distance = direction.magnitude;

        if (distance < 1 && follow.isGrounded)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            if (sprite.flipX)
            {
                rb.AddForce(new Vector2(-1.1f, 0.4f), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(1.1f, 0.4f), ForceMode2D.Impulse);
            }
            
        }


    }

    private IEnumerator Jump()
    {

        if (sprite.flipX)
        {
            Debug.Log("Jump");
            rb.AddForce(new Vector2(-2f, 6f), ForceMode2D.Impulse); ;
        }
        else
        {
            Debug.Log("Jump");
            rb.AddForce(new Vector2(2f, 6f), ForceMode2D.Impulse); ;
        }

        reachedDestination = false;
        yield return new WaitForSeconds(1);
        reachedDestination = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.y > follow.target.position.y && collision.transform == follow.target && motherMovement.wallCheck)
        {

            StartCoroutine(Jump());
        }

    }
}
