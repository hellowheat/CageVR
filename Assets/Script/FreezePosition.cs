using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePosition : MonoBehaviour
{
    Vector3 position;
    void Start()
    {
        position = transform.position;
    }
    
    void Update()
    {
        if(position != transform.position)
        {
            transform.position = position;
        }
    }
}
