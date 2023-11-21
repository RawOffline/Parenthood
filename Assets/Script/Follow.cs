using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Follow : MonoBehaviour
{
    public Transform target;  // The target character to follow
    public float followSpeed = 5f;
    public float stoppingDistance = 1f;

    public bool arrive = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            arrive = false;

        if (arrive == false)
            FollowParent();

    }

    public void FollowParent()
    {
        // Calculate the direction from the follower to the target
        Vector3 direction = target.position - transform.position;

        // Move the follower towards the target
        transform.Translate(direction.normalized * followSpeed * Time.deltaTime);

        // Check if the follower is close enough to the target to stop
        if (direction.magnitude <= stoppingDistance)
        {
            arrive = true;
        }
    }
}
