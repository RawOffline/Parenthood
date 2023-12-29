using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introScript : MonoBehaviour
{
    public GameObject child;
    public GameObject parent;
    public GameObject cutSceneChild;
    public GameObject cutSceneParent;
    public CinemachineVirtualCamera introVirtualCamera;
    private Follow follow;
    private MotherMovement motherMovement;
    void Start()
    {
        if (follow == null)
            follow = FindObjectOfType<Follow>();

        if (motherMovement == null)
            motherMovement = FindObjectOfType<MotherMovement>();
    }


    public void ChildMoveRight()
    {follow.movingRight = true;}
    public void DecreaseCameraPriority()
    {introVirtualCamera.Priority = 0;}
    public void IncreaseCameraPriority()
    { introVirtualCamera.Priority = 20; }
    public void MoveParent()
    {parent.transform.position = cutSceneChild.transform.position;}
    public void MoveChild()
    {child.transform.position = cutSceneParent.transform.position;}

    public void DeactivateChild()
    { child.SetActive(false); }

    public void DeactivateParent()
    { child.SetActive(false); }

    public void ActivateChild()
    { child.SetActive(true); }

    public void ActivateParent()
    { child.SetActive(true); }

    public void ActivateMovement() 
    { motherMovement.enabled = true;}
    public void DeactiavteMovement()
    { motherMovement.enabled = false; }

}
