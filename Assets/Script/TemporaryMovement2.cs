using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float groundCheckRadius = 0.1f;

    public float dirX = 0f;
    public bool isGrounded;
    private Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        Jump();
        GravityAdjust();
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            return;
        }

        if (Input.GetButtonUp("Jump") && rb2D.velocity.y > 0)
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.25f);
    }

    private void HorizontalMovement()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb2D.velocity = new Vector2(dirX * moveSpeed, rb2D.velocity.y);
    }

    private void GravityAdjust()
    {
        if (rb2D.velocity.y < 0)
            rb2D.gravityScale = 2.5f;

        else if (isGrounded)
            rb2D.gravityScale = 1;
    }
}
