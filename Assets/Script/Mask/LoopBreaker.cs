using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBreaker : MonoBehaviour
{
    public List<MaskListener> listeners;
    public MapMoveNew map;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            bool breakLoop = true;
            for (int i = 0; i < listeners.Count; i++)
                Debug.Log(listeners[i].isHideOut);
            for(int i=0; i < listeners.Count; i++)
            {
                if (!listeners[i].isHideOut)
                {
                    breakLoop = false;
                    break;
                }
            }
            if (breakLoop)
            {
                map.breakLoop();
            }
        }
    }
}
