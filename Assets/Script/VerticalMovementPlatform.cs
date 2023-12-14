using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VerticalMovementPlatform : MonoBehaviour
{
    public float endYPosition;         // Set the end Y-position in the Unity Editor
    public float moveDuration = 2f;     // Set the duration of the movement in the Unity Editor
    private float startYPosition;
    private bool isMoving = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            // Start moving the platform when the player enters the trigger
            StartCoroutine(MovePlatform());
        }
    }

    private IEnumerator MovePlatform()
    {
        isMoving = true;

        // Store the current position as the starting position
        startYPosition = transform.position.y;

        float timeElapsed = 0f;

        while (timeElapsed < moveDuration)
        {
            // Calculate the interpolation factor based on time
            float t = timeElapsed / moveDuration;

            // Apply easing or interpolation function as needed
            t = t * t * (3f - 2f * t);

            // Lerp between the start and end positions using the eased t
            float newYPosition = Mathf.Lerp(startYPosition, endYPosition, t);
            transform.position = new Vector2(transform.position.x, newYPosition);

            // Update the time elapsed
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        // Ensure the platform reaches the exact end position
        transform.position = new Vector2(transform.position.x, endYPosition);

        isMoving = false;
    }
}
