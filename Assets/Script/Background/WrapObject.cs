using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapObject : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 3f;
    public float horizontalWrapPosition = 29.6f;
    public float horizontalInitialPosition = -77.6f;
    public float verticalWrapPosition = 7.6f;
    public float verticalInitialPosition = -6f;

    public bool vertical = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vertical)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);

            if (transform.position.y > verticalWrapPosition)
                transform.position = new Vector2(transform.position.x, verticalInitialPosition);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x > horizontalWrapPosition)
                transform.position = new Vector2(horizontalInitialPosition, transform.position.y);
        }
    }
}
