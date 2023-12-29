using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testButton : MonoBehaviour
{
    public GameObject door;
    public GameObject layerWall;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 12)
        {
            layerWall.layer = 13;
            Destroy(door);
        }
    }
}
