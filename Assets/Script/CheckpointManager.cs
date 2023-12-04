using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    #region Singleton
    public static CheckpointManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    #endregion

    //public Transform defaultCheckPoint;
    // SceneHandler sceneHandler;

    public Transform[] childCheckpoints;
    public Transform[] parentCheckpoints;

    GameObject child;
    GameObject parent;

    int currentCheckpointIndex = 0;

    private void Start()
    {
        child = GameObject.FindGameObjectWithTag("child");
        parent = GameObject.FindGameObjectWithTag("Player");
    }

    public void LoadCheckpoint()
    {
        child.transform.position = childCheckpoints[currentCheckpointIndex].position;
        parent.transform.position = parentCheckpoints[currentCheckpointIndex].position;
    }

    public void SetCurrentCheckPoint(Transform checkpoint)
    {
        currentCheckpointIndex = Array.IndexOf(childCheckpoints, checkpoint);
    }
}
