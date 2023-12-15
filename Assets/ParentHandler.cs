using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentHandler : MonoBehaviour
{
    public void SetParent(Transform newParent)
    {
        transform.parent = newParent;
    }
}
