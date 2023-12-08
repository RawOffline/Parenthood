//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class StickyPlatform : MonoBehaviour
//{
//    private bool playerOnPlatform = false;
//    private Transform playerTransform;

//    private void Start()
//    {
      
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            playerOnPlatform = true;

//            MotherMovement motherControll = other.GetComponent<MotherMovement>();
//            if (motherControll != null)
//            {
//                playerTransform = motherControll.GetPlayerTransform();
//            }

            
//        }
//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            playerOnPlatform = false;
//            playerTransform = null;

//        }
//    }

//    private void LateUpdate()
//    {
//        // LateUpdate is called after all Update functions have been called
//        // Set the parent here to avoid the error
//        if (playerOnPlatform && playerTransform != null)
//        {
//            SetPlayerParent();
//        }
//    }

//    private void SetPlayerParent()
//    {
//        // Set the player as a child of the platform
//        playerTransform.SetParent(transform);
//    }
//}