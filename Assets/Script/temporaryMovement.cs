using UnityEngine;

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Crouch,
    Jump,
    Fall,
    Dead,
    Attack,
}

//This script is a clean powerful solution to a top-down movement player
public class temporaryMovement : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 5; //Our max speed
    public float acceleration = 20; //How fast we accelerate
    public float deacceleration = 4; //brake power

    internal float velocityX; //Our current velocity

    [Header("Jump")]
    public float jumpPower = 8; //How strong our jump is
    public float groundCheckDistance = 0.1f; //how far outsie our character we should raycast

    bool onGround = true; //checks if we are on the ground
    float groundCheckLenght; //Length of the raycast
    int maxJumps = 1; //We added a double jump to the game
    int currentJumps = 0;

    Rigidbody2D rb2D; //Ref to our rigidbody

    public PlayerState state = PlayerState.Idle;

    private void Start()
    {
        //Change project setting for raycast since they start inside colliders
        Physics2D.queriesStartInColliders = false;

        rb2D = GetComponent<Rigidbody2D>(); //assign our ref.

        //Calculate player size based on our colliders, lenght of raycast
        var collider = GetComponent<Collider2D>();
        groundCheckLenght = collider.bounds.size.y + groundCheckDistance;
    }

    void Update()
    {
        HorizontalMovement(); //Handles Horizontal movement

        Jump(); //handles jump

        GravityAdjust(); //adjusts gravity
    }

    private void GravityAdjust()
    {
        //If we are falling down increase gravity x4
        //This creates a much better feeling, less floaty
        if (rb2D.velocity.y < 0)
            rb2D.gravityScale = 4;
        else
            rb2D.gravityScale = 1;
    }

    private void Jump()
    {

        //if we press the button and have jumps remaining
        if (Input.GetButtonDown("Jump") && currentJumps < maxJumps)
        {
            currentJumps++;
            //Apply our jump power in the y direction
            var velocity = rb2D.velocity;
            velocity.y = jumpPower;
            rb2D.velocity = velocity;
            state = PlayerState.Jump;
            onGround = false;
            return;
        }

        if (Input.GetButtonUp("Jump") && rb2D.velocity.y > 0)
        {
            //Cut the jump short by reducing the upward velocity.
            //Same result as button down but on one line.
            rb2D.velocity = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.25f);
        }
        else if (rb2D.velocity.y < 0)
        {
            state = PlayerState.Fall;
        }

        //Raycast, check if there is anything under us.
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLenght);

        //Restet our counter if we are on the ground.
        if (onGround)
            currentJumps = 0;
    }

    private void HorizontalMovement()
    {
        //Get the raw input
        float x = Input.GetAxisRaw("Horizontal");

        //add our input to our velocity
        //This provides accelleration +10m/s/s
        velocityX += x * acceleration * Time.deltaTime;

        //Check our max speed, if our magnitude is faster them max speed
        velocityX = Mathf.Clamp(velocityX, -maxSpeed, maxSpeed);

        //If we have zero input from the player
        //(x < 0 == velocityX > 0) checks if input is in the opposite direction of movement
        if (x == 0 || (x < 0 == velocityX > 0))
        {
            //Reduce our speed based on how fast we are going
            //A value of 0.9 would remove 10% or our speed
            velocityX *= 1 - deacceleration * Time.deltaTime;
        }

        if (x == 0 && onGround && rb2D.velocity.y == 0)
        {
            state = PlayerState.Idle;
        }
        else if (onGround && rb2D.velocity.y == 0)
        {
            state = PlayerState.Run;
        }

        //Now we can move with the rigidbody and we get propper collisions
        rb2D.velocity = new Vector2(velocityX, rb2D.velocity.y);
    }
}