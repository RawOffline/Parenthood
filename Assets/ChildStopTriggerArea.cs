using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildStopTriggerArea : MonoBehaviour
{
    public GameObject child;
    public float moveSpeed = 0f;
    private bool triggered = false;
    Rigidbody2D rb;

    private void Start()
    {
        rb = child.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("child"))
        {
            triggered = true;
        }
        
    }

    private void Update()
    {

        if (triggered && child != null)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
