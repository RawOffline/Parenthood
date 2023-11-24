using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabChild : MonoBehaviour
{
    public GrabArea grabArea;
    public MotherMovementTemp motherMovement;
    public GameObject child;

    void Start()
    {
        grabArea = GetComponentInChildren<GrabArea>();
        motherMovement = GetComponent<MotherMovementTemp>();
    }

    void Update()
    {
        Debug.Log(grabArea.ChildGrab);

        if (grabArea.ChildGrab == true && !motherMovement.IsGrounded())
        {
            AttachChildToMother();
        }
    }

    void AttachChildToMother()
    {

        child.transform.SetParent(transform);


        child.transform.localPosition = new Vector2(0.8f, 0.8f);
    }
}
