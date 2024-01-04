using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingBlock : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject pointA;
    public GameObject pointB;
    private bool isGoingUp = false;
    private bool canHurtChild = false;
    private bool hitPlayer = false;

    private CheckpointManager checkpointManager;    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        checkpointManager = FindObjectOfType<CheckpointManager>();
    }

    private void Update()
    {
        if (isGoingUp)
        {
            canHurtChild = false;
            MoveUpToPointA();
        }
        else if (!isGoingUp && !hitPlayer)
        {
            canHurtChild = true;
            MoveUpToPointB();
            
        }
        
    }

    private void MoveUpToPointA()
    {
        Vector2 direction = (pointA.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(rb.velocity.x, direction.y * 2);

        if (transform.position.y >= pointA.transform.position.y - 0.2f)
        {
            rb.velocity = Vector2.zero;
            isGoingUp = false;
        }
    }

    private void MoveUpToPointB()
    {
        
        Vector2 direction = (pointB.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(rb.velocity.x, direction.y * 50);
        
        if (transform.position.y <= pointB.transform.position.y)
        {

            StartCoroutine(WaitAndMoveUp());
        }
    }

    private IEnumerator WaitAndMoveUp()
    {
        canHurtChild = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2);
        hitPlayer = false;
        isGoingUp = true;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("child") && canHurtChild)
        {
            checkpointManager.LoadCheckpoint();
            CinemachineShake.Instance.ShakeCamera(1f, 0.5f);
      
        }
        if (collision.gameObject.CompareTag("Player") && canHurtChild)
        {
            hitPlayer = true;
            StartCoroutine(WaitAndMoveUp());
            CinemachineShake.Instance.ShakeCamera(1f, 0.5f);
        }
    }

}
