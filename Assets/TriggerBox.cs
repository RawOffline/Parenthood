using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("child"))
        {
            CheckpointManager.Instance.SetCurrentCheckPoint(transform);
        }
    }
}
