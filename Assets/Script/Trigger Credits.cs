using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCredits : MonoBehaviour
{
    public GameObject parent;
    public GameObject credits;
    public GameObject sceneHandler;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateCredits();
        }
    }

    private void ActivateCredits()
    {
        if (parent != null)
        {
            Invoke("Fade", 40f);
            Credits script = credits.GetComponent<Credits>();

            if (script != null)
            {
                script.enabled = true;
            }
        }
    }

    void Fade()
    {
        if(sceneHandler != null)
        {
            SceneHandler sceneHandlerScript = sceneHandler.GetComponent<SceneHandler>();

            if (sceneHandlerScript != null)
            {
                sceneHandlerScript.NextScene();
            }
        }
    }
}
