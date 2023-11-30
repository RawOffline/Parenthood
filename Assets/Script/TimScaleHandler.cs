using System.Collections;
using UnityEngine;

public class TimeScaleHandler : MonoBehaviour
{
    public GameObject parent;
    MonoBehaviour[] scripts;

    private void Start()
    {
        scripts = parent.GetComponents<MonoBehaviour>();
        DisableScriptsOnParent();
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.002f * Time.timeScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 1f;
            EnableScriptsOnParent();
        }
    }

    private void DisableScriptsOnParent()
    {
        foreach (var script in scripts)
        {
            script.enabled = false;
        }
    }

    private void EnableScriptsOnParent()
    {
        Time.fixedDeltaTime = Time.deltaTime;
        foreach (var script in scripts)
        {
            script.enabled = true;
        }
    }
}
