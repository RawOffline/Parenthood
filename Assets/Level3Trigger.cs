using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Trigger : MonoBehaviour
{
    public SceneHandler sh;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("child") || collision.gameObject.CompareTag("Player"))
        {
            sh.NextScene();
        }
    }
}
