using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DistanceToPlatform : MonoBehaviour
{
    public Transform parent;
    public Transform smallPlatform;
    public Transform largePlatform;
    PlatMoveUpStop smallPlatformUp;
    PlatMoveUpStop largePlatformUp;
    private float activationDistanceSmall = 6f;
    private float activationDistanceLarge = 10.45f;
    public bool shouldMoveUp = false;
    private void Start()
    {
        smallPlatformUp = smallPlatform.GetComponent<PlatMoveUpStop>();
        largePlatformUp = largePlatform.GetComponent<PlatMoveUpStop>(); 
        if (smallPlatformUp == null && largePlatformUp == null ) 
        {
            print("Script cant be found!");
        }
    }


    // Update is called once per frame
    public void Update()
    {
        float distanceSmallPlatform = Vector2.Distance(parent.position, smallPlatform.position);
        float distanceLargePlatform = Vector2.Distance(parent.position, largePlatform.position);
       
        if(distanceSmallPlatform <= activationDistanceSmall)
        {
            smallPlatformUp.ActiveMoveUP();
        }

        if(distanceLargePlatform <= activationDistanceLarge)
        {
            largePlatformUp.ActiveMoveUP();
        }

        Debug.Log(distanceLargePlatform);
    }


}
