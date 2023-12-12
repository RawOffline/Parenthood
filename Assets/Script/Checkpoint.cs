using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform defaultCheckPoint;
    
    //public GameObject childPreFab;
    //public bool isChild;
    //public bool isParent;
    [SerializeField] Vector3 currentCheckPoint;

    GameObject player;
    //GameObject child;

    private void Awake()
    {
        currentCheckPoint = defaultCheckPoint.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetCheckpoint()
    {
        currentCheckPoint = player.transform.position;
    }

    public void RespawnPlayer()
    {
        if(player == null)
        {
            print("theres no player");
        }
        else
        {
            player.transform.position = currentCheckPoint;
        }
    }
    
}
