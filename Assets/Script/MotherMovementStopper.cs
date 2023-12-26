using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherMovementStopper : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MotherMovement motherMovement = collision.gameObject.GetComponent<MotherMovement>();
            motherMovement.onConveyer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MotherMovement motherMovement = collision.gameObject.GetComponent<MotherMovement>();
            motherMovement.onConveyer = false;
        }
    }


}
