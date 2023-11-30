using System.Collections;
using UnityEngine;

public class TimeScaleHandler : MonoBehaviour
{
    public GameObject parent;
    MonoBehaviour[] scripts;

    private void Start()
    {
        // Get all MonoBehaviours attached to the parent GameObject
        scripts = parent.GetComponents<MonoBehaviour>();

        // Disable all scripts on the parent GameObject
        DisableScriptsOnParent();

        // Set initial time scale
        Time.timeScale = 0.5f;

        // Adjust fixed delta time based on the time scale
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // Adjust this value based on your needs
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Restore normal time scale
            Time.timeScale = 1f;

            // Enable scripts on the parent GameObject
            EnableScriptsOnParent();
        }
    }

    private void DisableScriptsOnParent()
    {
        // Disable each script
        foreach (var script in scripts)
        {
            script.enabled = false;
        }
    }

    private void EnableScriptsOnParent()
    {
        // Enable each script
        foreach (var script in scripts)
        {
            script.enabled = true;
        }
    }


}
