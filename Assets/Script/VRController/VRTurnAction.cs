using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRTurnAction : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraObj;
    public CameraMask turnMask;
    public Vector3 turnAngle;

    void Start()
    {
        
    }
    
    public void turnFaceLeft()
    {
        turnMask.StartMask(() =>
        {
            rotate(-1 * turnAngle);
        }, null);
    }

    public void turnFaceRight()
    {
        turnMask.StartMask(() =>
        {
            rotate(turnAngle);
        }, null);
    }

    public void turnFaceBack()
    {
        Debug.Log(111);
        turnMask.StartMask(() =>
        {
            rotate(Vector3.up * 180) ;
        }, null);

    }



    void rotate(Vector3 angle)
    {
        Vector3 oldCamPosition = cameraObj.transform.position;
        player.transform.Rotate(angle);
        player.transform.position += oldCamPosition - cameraObj.transform.position;
    }
}
