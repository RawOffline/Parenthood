using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ChildAnimationEvents;

public class ChangeStateParentChildMoveRight : MonoBehaviour
{
    private ChildAnimationEvents childAnimationEvents;

    void Start()
    {

        childAnimationEvents = FindObjectOfType<ChildAnimationEvents>();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (childAnimationEvents != null && other.CompareTag("Player"))
        {
            childAnimationEvents.ChangeState(childState.MovingRight);
        }
    }
}
