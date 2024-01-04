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
    public SceneHandler sceneHandler;
    void Start()
    {
        sceneHandler = FindAnyObjectByType<SceneHandler>();
        if (follow == null)
            follow = FindObjectOfType<Follow>();

        if (motherMovement == null)
            motherMovement = FindObjectOfType<MotherMovement>();
    }


    public void ChildMoveRight()
    { follow.movingRight = true; }
    public void DecreaseCameraPriority()
    { introVirtualCamera.Priority = 0; }
    public void IncreaseCameraPriority()
    { introVirtualCamera.Priority = 21; }
    public void MoveParent()
    { parent.transform.position = cutSceneParent.transform.position; }
    public void MoveChild()
    { child.transform.position = cutSceneChild.transform.position; }



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
    { motherMovement.enabled = true; }
    public void DeactiavteMovement()
    { motherMovement.enabled = false; }

    public void LoadLastScene()
    { sceneHandler.NextScene(); }

    public void WindMusic()
    { SoundManager.PlaySound(SoundManager.Sound.CutsceneWind, false, false); }

    public void cutSceneAudio_Level_1()
    {
        SoundManager.PlaySound(SoundManager.Sound.Cutscene_SoundAudio_level_1, false, false);
    }

    public void cutSceneAudio_Level_3()
    {
        SoundManager.PlaySound(SoundManager.Sound.Cutscene_SoundAudio_level_3, false, false);
    }

    public void cutSceneAudio_Level_2_2()
    {
        SoundManager.PlaySound(SoundManager.Sound.Cutscene_SoundAudio_level_2_2, false, false);
    }


}
