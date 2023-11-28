using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 15f;
    public bool reverse = false;

    void Update()
    {
        if (reverse)
            transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
        else
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
