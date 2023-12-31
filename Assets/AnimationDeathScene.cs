using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDeathScene : MonoBehaviour
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
            child.transform.DOMoveX(cutSceneChild.transform.position.x, 1);
            parent.transform.DOMoveX(cutSceneParent.transform.position.x, 1).OnComplete(PlayAnimation);
        }

    }

    private void PlayAnimation()
    {
        animator.SetTrigger("DeathSceneTrigger");
    }
}
