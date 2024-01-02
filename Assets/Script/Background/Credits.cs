using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        // Move the object upwards on the Y-axis
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
