using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveRightTrigger : MonoBehaviour
{
    Follow follow;
    void Start()
    {
        follow = FindObjectOfType<Follow>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            follow.movingLeft = false;
            follow.movingRight = true;
            Destroy(gameObject);
        }
    }
}
