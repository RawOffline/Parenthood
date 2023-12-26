using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRaft : MonoBehaviour
{
    public MovingPlatform mp;

    private void Start()
    {
        mp = GetComponent<MovingPlatform>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("child"))
        {
            mp.enabled = true;
        }
    }
}
