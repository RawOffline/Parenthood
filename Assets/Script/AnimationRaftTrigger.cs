using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimationRaftTrigger : MonoBehaviour
{
    public GameObject child;
    public GameObject parent;
    public GameObject cutSceneChild;
    public GameObject cutSceneParent;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        child.transform.DOMoveX(cutSceneChild.transform.position.x, 1);
        parent.transform.DOMoveX(cutSceneParent.transform.position.x, 1).OnComplete(PlayAnimation);
    }

    private void PlayAnimation()
    {
        animator.SetTrigger("RaftTrigger");
    }
}
