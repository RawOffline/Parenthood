using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeath : MonoBehaviour
{
    private CheckpointManager checkpoint;

    void Start()
    {
        checkpoint = FindObjectOfType<CheckpointManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("child") || collision.CompareTag("Player"))
        {
            checkpoint.LoadCheckpoint();
        }
    }
}
