using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    {parent.transform.position = cutSceneParent.transform.position;}
    public void MoveChild()
    {child.transform.position = cutSceneChild.transform.position;}



    public void DeactivateChild()
    { child.SetActive(false); }

    public void DeactivateParent()
    { parent.SetActive(false); }

    public void ActivateChild()
    { child.SetActive(true); }
    public void ActivateParent()
    { parent.SetActive(true); }


    public void ActivateCutSceneParent()
    { cutSceneParent.SetActive(true); }

    public void DeactivateCutSceneChild()
    { cutSceneChild.SetActive(false); }

    public void DeactivateCutSceneParent()
    { cutSceneParent.SetActive(false); }

    public void ActivateCutSceneChild()
    { cutSceneChild.SetActive(true); }



    public void ActivateMovement() 
    { motherMovement.enabled = true;}
    public void DeactiavteMovement()
    { motherMovement.enabled = false; }

    public void LoadLastScene()
    { SceneManager.LoadScene("Credits");}

    //public void ShowBars()
    //{
    //    if (CinematicBarsController.Instance != null)
    //    CinematicBarsController.Instance.ShowBars();
    //}
    //public void HideBars()
    //{
    //    if (CinematicBarsController.Instance != null)
    //        CinematicBarsController.Instance.HideBars();
    //}

}
