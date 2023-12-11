using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ChildScriptedEvents : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool reachedDestination = true;

    private MotherMovement motherMovement;
    private Follow follow;
    private SpriteRenderer sprite;

    

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

        if (motherMovement.wallCheck == true && reachedDestination && motherMovement.IsGrounded() && transform.position.y < follow.target.position.y)
        {
            ParentAgainstWall();
        }

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
                rb.AddForce(new Vector2(-1f, 0.2f), ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(new Vector2(1f, 0.2f), ForceMode2D.Impulse);
            }
            
        }


    }

    private IEnumerator Jump()
    {

        if (sprite.flipX)
        {
            Debug.Log("Jump");
            rb.AddForce(new Vector2(-2f, 7f), ForceMode2D.Impulse); ;
        }
        else
        {
            Debug.Log("Jump");
            rb.AddForce(new Vector2(2f, 7f), ForceMode2D.Impulse); ;
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
