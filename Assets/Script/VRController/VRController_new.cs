using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController_new : MonoBehaviour
{
    public SteamVR_Action_Boolean teleport = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    public SteamVR_Action_Boolean moveBack = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("MoveBack");
    public SteamVR_Action_Boolean turnLeft = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("TurnLeft");
    public SteamVR_Action_Boolean turnRight = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("TurnRight");
    public SteamVR_Action_Boolean trigger = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");
    public SteamVR_Action_Boolean grip = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    public Interactor interactor;
    SteamVR_Behaviour_Pose behaviourPose;

    VRMoveAction moveAction;
    VRTurnAction turnAction;
    void Start()
    {
        behaviourPose = GetComponent<SteamVR_Behaviour_Pose>();
        moveAction = GetComponent<VRMoveAction>();
        turnAction = GetComponent<VRTurnAction>();
    }
    
    void Update()
    {
        if (teleport.GetStateDown(behaviourPose.inputSource))moveAction.startShowMove();
        if (teleport.GetStateUp(behaviourPose.inputSource))moveAction.endShowMove();
        
        if (moveBack.GetStateDown(behaviourPose.inputSource)) moveAction.MoveBack();
        if (turnLeft.GetStateDown(behaviourPose.inputSource))turnAction.turnFaceLeft();
        if (turnRight.GetStateDown(behaviourPose.inputSource))turnAction.turnFaceRight();

        if (trigger.GetStateDown(behaviourPose.inputSource)) interactor.InteractorActionStart();
        if (trigger.GetStateUp(behaviourPose.inputSource)) interactor.InteractorActionEnd();
    }
}
