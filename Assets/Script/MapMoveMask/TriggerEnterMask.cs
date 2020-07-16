using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnterMask : MonoBehaviour
{
    [HideInInspector]
    public bool isTriggerEnter = false;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.CompareTo("MainCamera") == 0)
        {
            isTriggerEnter = true;
        }
    }
}
