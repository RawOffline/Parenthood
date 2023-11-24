using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabArea : MonoBehaviour
{
    public bool ChildGrab;

    private void Start()
    {
        ChildGrab = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("child"))
        {
            ChildGrab = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("child"))
        {
            ChildGrab = false;
        }
    }
}
