using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ChildMoveCorners : MonoBehaviour
{
    RaycastHit2D hit;
    RaycastHit2D hit2;
    Rigidbody2D rb;
    [SerializeField] private LayerMask AllWalls;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RayCastCorners();
        if (hit.collider)
        {
            rb.AddForce(-transform.right / 5, ForceMode2D.Impulse);
        }
        if (hit2.collider)
        {
            rb.AddForce(transform.right / 5, ForceMode2D.Impulse);
        }
    }

    private void RayCastCorners()
    {
        hit = Physics2D.Raycast(transform.position, new Vector2(0.5f, 0), 0.1f, AllWalls);
        hit2 = Physics2D.Raycast(transform.position, new Vector2(-0.5f, 0), 0.1f, AllWalls);
    }

}

