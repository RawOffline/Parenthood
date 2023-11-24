using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMoveTriggerArea : MonoBehaviour
{
    public GameObject child;
    public float moveSpeed = 5f;
    private bool triggered = false;
    public Rigidbody2D rb;

    private void Start()
    {
        triggered = false;
        rb = child.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
        }
        else if (collision.gameObject.CompareTag("child"))
        {
            triggered = false;

        }
    }


    private void Update()
    {
        Debug.Log(triggered);
        if (triggered)
        {

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        }
        else if (!triggered)
        {

            rb.velocity = new Vector2(0, rb.velocity.y);

        }
    }
}
