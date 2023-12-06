using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DistanceToPlatform : MonoBehaviour
{
    public Transform Child;
    public Transform Platform;
    public float squareDistance;
    public float distance;
    PlatMoveUpStop platformUp;
    private void Start()
    {
        platformUp = GetComponent<PlatMoveUpStop>();
    }


    // Update is called once per frame
    void Update()
    {
         if (Child != null && Platform != null)
         {
            squareDistance = (Child.position - Platform.position).sqrMagnitude;
            distance = Mathf.Sqrt(squareDistance);

            Debug.Log(distance);
         }
        if (distance< 15)
        {
            platformUp.MoveUP();
        }
        
    }


}
