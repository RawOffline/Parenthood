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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGoingUp)
        {
            MoveUpToPointA();
        }
        else
        {
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
        rb.velocity = new Vector2(rb.velocity.x, direction.y * 100);

        if (transform.position.y <= pointB.transform.position.y)
        {
            StartCoroutine(WaitAndMoveUp());
        }
    }

    private IEnumerator WaitAndMoveUp()
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2);
        isGoingUp = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("child"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitAndMoveUp());
        }
    }
}
