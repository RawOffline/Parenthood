using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introScript : MonoBehaviour
{
    public CinemachineVirtualCamera introVirtualCamera;
    private Follow follow;

    void Start()
    {
        follow = FindObjectOfType<Follow>();
    }

    public void ChildMoveRight()
    {
        follow.movingRight = true;
    }
    public void ActivateChildAndParent()
    {
        introVirtualCamera.Priority = 0;
    }

    
}
