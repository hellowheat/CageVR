using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoParentUpdate : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform parentTransfom= hasTagParent(collision.transform, "MinMap");
        if (parentTransfom != null)
        {
            Transform autoParent = parentTransfom.Find("autoParent");
            if(autoParent != null)
            {
                transform.parent = autoParent;
            }
        }
    }

    Transform hasTagParent(Transform t, string tagStr)
    {
        if (t.tag.CompareTo(tagStr) == 0) return t;
        if (t.parent == null) return null;
        return hasTagParent(t.parent, tagStr);
    }
}
