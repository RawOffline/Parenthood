using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCollapsingBridge : MonoBehaviour
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
        if (collision.gameObject.layer == 11)
        {
            child.transform.DOMove(cutSceneChild.transform.position, 3);
            parent.transform.DOMove(cutSceneParent.transform.position, 3).OnComplete(PlayAnimation);
        }

    }

    private void PlayAnimation()
    {
        animator.SetTrigger("BridgeTrigger");
    }
}
