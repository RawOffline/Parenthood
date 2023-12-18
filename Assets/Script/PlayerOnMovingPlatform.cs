using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnMovingPlatform : MonoBehaviour
{
    public bool playerOnPlatform = false;
    private Transform playerTransform;
    public float gravityScale;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
     
            playerOnPlatform = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            //playerOnPlatform = false;
     
            //playerTransform = null;
            playerOnPlatform = false;
            
        }
    }

    private void LateUpdate()
    {
        // LateUpdate is called after all Update functions have been called
        // Set the parent here to avoid the error
        if (playerOnPlatform && playerTransform != null)
        {
            SetPlayerParent();
        }
    }

    private void SetPlayerParent()
    {
        // Set the player as a child of the platform
        playerTransform.SetParent(transform);
    }
}
