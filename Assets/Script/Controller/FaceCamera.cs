using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    Camera faceObject;
    public bool freezeX;
    public bool freezeY;
    public bool freezeZ;
    public float scaleX = 1;
    public float scaleY = 1;
    public float scaleZ = 1;
    //    public float UpdateDelayTime = 0.3f;
    void Start()
    {
        faceObject = Camera.main;
    }
    
    void Update()
    {
        if (faceObject)
        {
            transform.forward = new Vector3((!freezeX? faceObject.transform.position.x - transform.position.x:transform.forward.x)* scaleX,
                (!freezeY ? faceObject.transform.position.y - transform.position.y : transform.forward.y)* scaleY,
                (!freezeZ ? faceObject.transform.position.z - transform.position.z : transform.forward.z)* scaleZ);
        }
    }
}
