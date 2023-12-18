using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStickyPlatform : MonoBehaviour
{
    // When gameobject collides with another gameobject that has a trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the collided gameObject has the name "Player"
        if (collision.gameObject.name == "Parent" || collision.gameObject.name == "Child")
        {
            // Turn the players transform into the platforms transform by becoming its parent
            collision.gameObject.transform.SetParent(transform);
        }
    }

    // When gameobject that has collided with another gameobject (with trigger) exits
    private void OnTriggerExit2D(Collider2D collision)
    {
        // If the gameObject has the name "Player"
        if (collision.gameObject.name == "Parent" || collision.gameObject.name == "Child")
        {
            // Set the parent to null
            collision.gameObject.transform.SetParent(null);
        }
    }
}
