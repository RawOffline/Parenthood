using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpFallMultiplier = 2f;
    Rigidbody2D rb;
    MotherMovement motherMovement;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        motherMovement = GetComponent<MotherMovement>();
       
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (rb.velocity.y < 2.5f)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        

        if (motherMovement.IsGrounded())
        {
            rb.gravityScale = 1f;
        }

        if(motherMovement.isGodMode == true)
        {
            rb.gravityScale = 0;
        }
    }
}
