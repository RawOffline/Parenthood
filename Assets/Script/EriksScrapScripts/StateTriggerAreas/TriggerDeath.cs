using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDeath : MonoBehaviour
{
    private SceneHandler sceneHandler;

    void Start()
    {
        sceneHandler = FindObjectOfType<SceneHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("child") || collision.CompareTag("Player"))
        {
            sceneHandler.Restart();
        }
    }
}
