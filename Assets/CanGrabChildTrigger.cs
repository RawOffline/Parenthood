using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGrabChildTrigger : MonoBehaviour
{
    public GameObject grabArea;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            grabArea.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            grabArea.SetActive(true);
        }
        
    }
}
