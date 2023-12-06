using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMoveUpStop : MonoBehaviour
{
    public float speed = 1f;
    public float maxHeight = 5.0f;

    private bool shouldMoveUp = false;

    private void Update()
    {
        if (shouldMoveUp)
        {
            MoveUP();
        }
    }

    public void ActiveMoveUP()
    {
        shouldMoveUp = true;

    }
    public void MoveUP()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.y >= maxHeight)
        {
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }
    }
}
