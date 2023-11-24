using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrabChild : MonoBehaviour
{
    public GrabArea grabArea;
    private MotherMovementTemp motherMovement;
    public GameObject child;
    private Rigidbody2D childRb;
    private Rigidbody2D motherRb;
    private float forceDecrease = 4.5f;
    private int repeatCount = 0; // Variable to track the number of repetitions

    void Start()
    {
        grabArea = GetComponentInChildren<GrabArea>();
        motherMovement = GetComponent<MotherMovementTemp>();
        childRb = child.GetComponent<Rigidbody2D>();
        motherRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (grabArea.ChildGrab && !motherMovement.IsGrounded() && child != null)
        {
            AttachChildToMother();
            childRb.isKinematic = true;
        }
        else if (motherMovement.IsGrounded() && child != null)
        {
            DetachChildFromMother();
            childRb.isKinematic = false;
        }


        
    }

    void AttachChildToMother()
    {
        if (child != null)
        {
            child.transform.SetParent(transform);
            MoveChildInAir();
        }
    }

    void DetachChildFromMother()
    {
        if (child != null && child.transform.parent == transform)
        {

            child.transform.SetParent(null);

            CancelInvoke("AddForceToChild");

            InvokeRepeating("AddForceToChild", 0, 0.2f);
        }
    }

    void AddForceToChild()
    {
        
        childRb.AddForce(new Vector2(transform.localScale.x * forceDecrease, forceDecrease / 2f), ForceMode2D.Impulse);

        forceDecrease -= 1.5f;


        repeatCount++;
        if (repeatCount >= 3)
        {

            CancelInvoke("AddForceToChild");
            forceDecrease = 3;
            repeatCount = 0;
        }
    }

    void MoveChildInAir()
    {
        Vector2 startPosition = child.transform.localPosition;

        child.transform.localPosition = Vector2.Lerp(startPosition, new Vector2(0.7f, 0f), Time.deltaTime*7);
        

        Debug.Log(startPosition);
    }
}
