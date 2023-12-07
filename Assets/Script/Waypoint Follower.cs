using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    // Array for the waypoints
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    // The moving gameObjects speed
    [SerializeField] private float speed = 2f;

    void Update()
    {
        // If the distance between the waypoints are smaller than .1f
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            // Check the next waypoint
            currentWaypointIndex++;

            // If the index of the waypoints are larger or equal to the arrays length
            if (currentWaypointIndex >= waypoints.Length)
            {
                // Back to zero (the first waypoint)
                currentWaypointIndex = 0;
            }
        }

        if (currentWaypointIndex != 0)
        {
            // Move the platform towards the waypoints which will alternate when reaching an end
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
    }
}
