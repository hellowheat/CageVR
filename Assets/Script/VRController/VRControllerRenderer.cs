using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_Behaviour_Pose))]
public class VRControllerRenderer : MonoBehaviour
{
    public GameObject player;
    public Interactor interactor;
    public float speed;
    public SteamVR_Action_Boolean triggerBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Action_Boolean gripBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public SteamVR_Action_Boolean teleportBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    public SteamVR_Action_Boolean moveForwradBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("MoveForward");
    public SteamVR_Action_Boolean moveBackBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("MoveBack");
    public SteamVR_Action_Boolean moveLeftBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("MoveLeft");
    public SteamVR_Action_Boolean moveRightBtn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("MoveRight");
    private SteamVR_Behaviour_Pose pose;
    private Rigidbody controllRb;

    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        controllRb = player.GetComponent<Rigidbody>();
    }
    
    void Update() 
    {
        if(triggerBtn != null && triggerBtn.GetStateDown(pose.inputSource))
        {
            interactor.InteractorActionStart();
        }
        if (triggerBtn != null && triggerBtn.GetStateUp(pose.inputSource))
        {
            interactor.InteractorActionEnd();
        }
        if (gripBtn != null && gripBtn.GetState(pose.inputSource))
        {
            Debug.Log("grid down");
        }
        Debug.Log("teleportBtn:" + teleportBtn.state);
        if(moveForwradBtn != null && moveForwradBtn.GetState(pose.inputSource))
        {
            controllRb.AddForce(transform.forward.normalized * speed);
        }
        if (moveLeftBtn != null && moveLeftBtn.GetState(pose.inputSource))
        {
            controllRb.AddForce(Quaternion.AngleAxis(-90, Vector3.up) * transform.forward.normalized * speed);
        }
        if (moveBackBtn != null && moveBackBtn.GetState(pose.inputSource))
        {
            controllRb.AddForce(Quaternion.AngleAxis(180, Vector3.up) * transform.forward.normalized * speed);
        }
        if (moveRightBtn != null && moveRightBtn.GetState(pose.inputSource))
        {
            controllRb.AddForce(Quaternion.AngleAxis(90, Vector3.up) * transform.forward.normalized * speed);
        }
    } 
}
