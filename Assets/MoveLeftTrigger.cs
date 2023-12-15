using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftTrigger : MonoBehaviour
{
    Follow follow;
    void Start()
    {
        follow = FindObjectOfType<Follow>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
        if (collision.gameObject.layer == 12)
        {
            follow.movingRight = false;
            follow.movingLeft = true;
            Destroy(gameObject);
        }
    }
}
