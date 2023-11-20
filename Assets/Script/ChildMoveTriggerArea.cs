using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMoveTriggerArea : MonoBehaviour
{
    public GameObject child;
    public float moveSpeed = 5f; 
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        triggered = true;
    }

    private void Update()
    {
        
        if (triggered && child != null)
        {
            
            Vector3 newPosition = child.transform.position + new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
            child.transform.position = newPosition;
        }
    }
}
