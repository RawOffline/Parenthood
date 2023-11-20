using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crushingBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("child"))
        {
            Destroy(collision.gameObject);
        }
    }
}
