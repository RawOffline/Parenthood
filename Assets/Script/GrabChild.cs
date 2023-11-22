using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabChild : MonoBehaviour
{
    public GrabArea grabArea; // Assuming GrabArea is a script attached to the same GameObject
    public MotherMovementTemp motherMovement; // Assuming MotherMovementTemp is a script attached to the same GameObject
    public GameObject child;

    void Start()
    {
        grabArea = GetComponentInChildren<GrabArea>();
        motherMovement = GetComponent<MotherMovementTemp>();
    }

    void Update()
    {

        if (grabArea.ChildGrab == true && !motherMovement.IsGrounded())
        {
            Debug.Log("grab");
            child.transform.position = new Vector2(transform.position.x + 1, transform.position.y);
        }

        else if (grabArea.ChildGrab == true && motherMovement.IsGrounded())
        {
            grabArea.ChildGrab = false;
        }
    }
}

