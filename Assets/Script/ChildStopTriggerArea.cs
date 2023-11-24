using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildStopTriggerArea : MonoBehaviour
{
    public GameObject child;
    Rigidbody2D rb;

    private void Start()
    {
        rb = child.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("child"))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
