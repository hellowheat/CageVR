using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSetter : MonoBehaviour
{
    public MapMoveNew map;
    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.CompareTo("Player") == 0)
        {
            map.setLoop();
        }
    }
}
