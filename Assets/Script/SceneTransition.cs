using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        //print("animator is on: " + animator.name);
    }

    public void PlayStartTransition()
    {
        animator.SetTrigger("Start");
    }
    public void PlayEndTransition()
    {
        animator.SetTrigger("End");
    }
}