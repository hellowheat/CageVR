using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Camera faceObject;
    public bool freezeX;
    public bool freezeY;
    public bool freezeZ;
    //    public float UpdateDelayTime = 0.3f;
    void Start()
    {
        faceObject = Camera.main;
    }
    
    void Update()
    {
        if (faceObject)
        {
            transform.forward = new Vector3(!freezeX?transform.position.x - faceObject.transform.position.x:transform.forward.x,
                !freezeY ? transform.position.y - faceObject.transform.position.y : transform.forward.y,
                !freezeZ ? transform.position.z - faceObject.transform.position.z : transform.forward.z);
        }
    }
}
