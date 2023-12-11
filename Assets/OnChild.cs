using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChild : MonoBehaviour
{
    public GameObject child;
    public GameObject parent;
    Collider2D childCollider;
    Collider2D parentCollider;

    private void Start()
    {
        // Assuming that the colliders are on the child and parent GameObjects
        childCollider = child.GetComponent<Collider2D>();
        parentCollider = parent.GetComponent<Collider2D>();

        // If the colliders are on the same GameObject as the script, you can use this.gameObject
        // childCollider = GetComponent<Collider2D>();
        // parentCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("child"))
        {
            Debug.Log("Ignoring collision");
            Physics2D.IgnoreCollision(childCollider, parentCollider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("child"))
        {
            Debug.Log("Resuming collision");
            Physics2D.IgnoreCollision(childCollider, parentCollider, false);
        }
    }

}
